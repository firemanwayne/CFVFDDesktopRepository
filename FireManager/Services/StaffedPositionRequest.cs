using FireManager.Concrete;
using FireManager.Entities.StaffedPositionAggregate;
using FireManager.Extensions;
using FireManager.Interface;
using FireManager.Queries;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FireManager.Services
{
    public class StaffedPositionRequest : IStaffedPositionRequest
    {
        private readonly IRequests Requests;
        private readonly FireManagerOptions Options;
        private readonly IHttpClientFactory ClientFactory;

        public StaffedPositionRequest(
            IRequests Requests,
            IHttpClientFactory ClientFactory,
            IOptions<FireManagerOptions> Options)
        {
            this.Requests = Requests;
            this.Options = Options.Value;
            this.ClientFactory = ClientFactory;
        }

        public async Task<Stream> StreamStaffedPositionsAsync(DateTime RequestDate)
        {
            try
            {
                var Client = ClientFactory.CreateClient();
                var Content = new FormUrlEncodedContent(Requests.AllSchedulesByDate(RequestDate));

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
        public async Task<Stream> StreamStaffedPositionsAsync(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var Client = ClientFactory.CreateClient();
                var Content = new FormUrlEncodedContent(Requests.AllSchedulesByDateRange(StartDate, EndDate));

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
        public async Task<Stream> StreamStaffedPositionsAsync(int Month, int Year)
        {
            try
            {
                var Client = ClientFactory.CreateClient();
                var Content = new FormUrlEncodedContent(Requests.AllSchedulesByMonth(new DateTime(Year, Month, 1)));
                
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

        public async Task<IList<FireManagerStaffedPosition>> GetStaffedPositionsAsync(DateTime RequestDate)
        {
            IList<FireManagerStaffedPosition> FireManagerStaffedPositions = new List<FireManagerStaffedPosition>();
            var Serializer = new XmlSerializer(typeof(Results));

            using (var xReader = XmlReader.Create(await StreamStaffedPositionsAsync(RequestDate)))
            {
                var Results = (Results)Serializer.Deserialize(xReader);

                foreach (var ResultRange in Results.ResultsRanges.Range)
                    FireManagerStaffedPositions.Add(FireManagerStaffedPosition.Instance(ResultRange.Schedule, ResultRange.Position, ResultRange.Member, ResultRange.Begin, ResultRange.End));

                return FireManagerStaffedPositions;
            }
        }
        public async Task<IList<FireManagerStaffedPosition>> GetStaffedPositionsAsync(DateTime StartDate, DateTime EndDate)
        {
            IList<FireManagerStaffedPosition> FireManagerStaffedPositions = new List<FireManagerStaffedPosition>();
            var Serializer = new XmlSerializer(typeof(Results));

            using (var xReader = XmlReader.Create(await StreamStaffedPositionsAsync(StartDate, EndDate)))
            {
                var Results = (Results)Serializer.Deserialize(xReader);

                foreach (var ResultRange in Results.ResultsRanges.Range)
                    FireManagerStaffedPositions.Add(FireManagerStaffedPosition.Instance(ResultRange.Schedule, ResultRange.Position, ResultRange.Member, ResultRange.Begin, ResultRange.End));

                return FireManagerStaffedPositions;
            }
        }
        public async Task<IList<FireManagerStaffedPosition>> GetStaffedPositionsAsync(int Month, int Year)
        {
            IList<FireManagerStaffedPosition> FireManagerStaffedPositions = new List<FireManagerStaffedPosition>();
            var Serializer = new XmlSerializer(typeof(Results));

            using (var xReader = XmlReader.Create(await StreamStaffedPositionsAsync(Month, Year)))
            {
                var Results = (Results)Serializer.Deserialize(xReader);

                foreach (var ResultRange in Results.ResultsRanges.Range)
                    FireManagerStaffedPositions.Add(FireManagerStaffedPosition.Instance(ResultRange.Schedule, ResultRange.Position, ResultRange.Member, ResultRange.Begin, ResultRange.End));

                return FireManagerStaffedPositions;
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
