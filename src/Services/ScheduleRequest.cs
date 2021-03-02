using FireManager.Abstract;
using FireManager.Concrete;
using FireManager.Entities;
using FireManager.Extensions;
using FireManager.Interface;
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
    internal class ScheduleRequest : RequestBase, IScheduleRequest
    {
        public ScheduleRequest(
            IRequests Requests,
            IHttpClientFactory Factory,
            IOptions<FireManagerOptions> Options) : base(Requests, Factory, Options)
        { }

        public async Task<Stream> StreamSchedulesAsync()
        {
            try
            {
                var Content = new FormUrlEncodedContent(Requests.AllSchedulesRequest);

                var Client = Factory.CreateClient();
                var Message = CreatePostMessage(Options.Url, Content);
                var Response = await Client.SendAsync(Message, HttpCompletionOption.ResponseHeadersRead);
                return await Response.Content.ReadAsStreamAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fire Manager Request Error: {ex.Message}");
                return null;
            }
        }

        public async IAsyncEnumerable<FireManagerSchedule> GetSchedulesAsync()
        {
            var Serializer = new XmlSerializer(typeof(Results));
            using var xReader = XmlReader.Create(await StreamSchedulesAsync());
            var Results = (Results)Serializer.Deserialize(xReader);

            if (Results != null)
            {
                var ScheduleResults = Results
                    .Schedules
                    .Schedule
                    .ToList();

                foreach (var Schedule in ScheduleResults)
                    yield return FireManagerSchedule.Instance(Schedule.Id, Schedule.Name.Value);
            }
        }
    }
}
