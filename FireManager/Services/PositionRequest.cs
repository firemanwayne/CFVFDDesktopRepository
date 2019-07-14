using FireManager.Concrete;
using FireManager.Entities.PositionAggregate;
using FireManager.Entities.ScheduleAggregate;
using FireManager.Extensions;
using FireManager.Interface;
using FireManager.Queries;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FireManager.Services
{
    public class PositionRequest : IPositionRequest
    {
        private readonly IRequests Requests;
        private readonly FireManagerOptions Options;
        private readonly IHttpClientFactory ClientFactory;

        public PositionRequest(
            IRequests Requests,
            IHttpClientFactory ClientFactory,
            IOptions<FireManagerOptions> Options)
        {
            this.Requests = Requests;
            this.Options = Options.Value;
            this.ClientFactory = ClientFactory;
        }

        public async Task<Stream> StreamPositionsAsync()
        {
            try
            {
                var Content = new FormUrlEncodedContent(Requests.AllSchedulesRequest);

                var Client = ClientFactory.CreateClient();
                var Message = CreatePostMessage(Options.Url, Content);
                var Response = await Client.SendAsync(Message, HttpCompletionOption.ResponseHeadersRead);

                return await Response.Content.ReadAsStreamAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Fire Manager Request Error: {ex.Message}");
                return null;
            }
        }
        public async Task<IList<FireManagerPosition>> GetPositionsAsync()
        {
            IList<FireManagerPosition> Positions = new List<FireManagerPosition>();
            var Serializer = new XmlSerializer(typeof(Results));

            using (var xReader = XmlReader.Create(await StreamPositionsAsync()))
            {
                var Results = (Results)Serializer.Deserialize(xReader);

                if (Results != null)
                    foreach (var Schedule in Results.Schedules.Schedule.ToList())
                        foreach (var Position in Schedule.Positions.ToList())
                            foreach (var item in Position.Position.ToList())
                            {
                                var schedule = FireManagerSchedule.Instance(Schedule.Id, Schedule.Name.Value);
                                var position = FireManagerPosition.Instance(Schedule, item);

                                Positions.Add(position);
                            }

                return Positions;
            }         
        }

        private static HttpRequestMessage CreatePostMessage(string Path, FormUrlEncodedContent Content)
        {
            var Message = new HttpRequestMessage();
            Message.RequestUri = new Uri(Path);
            Message.Method = HttpMethod.Post;
            Message.Content = Content;

            return Message;
        }
    }
}
