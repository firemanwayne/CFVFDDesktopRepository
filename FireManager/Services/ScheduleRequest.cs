using FireManager.Concrete;
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
    public class ScheduleRequest : IScheduleRequest
    {                
        private readonly IRequests Requests;
        private readonly FireManagerOptions Options;
        private readonly IHttpClientFactory ClientFactory;

        public ScheduleRequest(
            IRequests Requests,
            IHttpClientFactory ClientFactory,
            IOptions<FireManagerOptions> Options)
        {            
            this.Requests = Requests;
            this.Options = Options.Value;
            this.ClientFactory = ClientFactory;
        }

        public async Task<Stream> StreamSchedulesAsync()
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

        public async Task<IList<FireManagerSchedule>> GetSchedulesAsync()
        {
            IList<FireManagerSchedule> Schedules = new List<FireManagerSchedule>();

            var Serializer = new XmlSerializer(typeof(Results));
            using (var xReader = XmlReader.Create(await StreamSchedulesAsync()))
            {
                var Results = (Results)Serializer.Deserialize(xReader);

                if (Results != null)
                {
                    var ScheduleResults = Results
                        .Schedules
                        .Schedule
                        .ToList();

                    foreach (var Schedule in ScheduleResults)
                        Schedules.Add(FireManagerSchedule.Instance(Schedule.Id, Schedule.Name.Value));
                }
                return Schedules;
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
