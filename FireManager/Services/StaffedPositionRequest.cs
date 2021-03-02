using FireManager.Abstract;
using FireManager.Concrete;
using FireManager.Entities;
using FireManager.Extensions;
using FireManager.Interface;
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
    internal class StaffedPositionRequest : RequestBase, IStaffedPositionRequest
    {
        public StaffedPositionRequest(
            IRequests Requests,
            IHttpClientFactory Factory,
            IOptions<FireManagerOptions> Options) : base(Requests, Factory, Options)
        { }

        public async Task<Stream> StreamStaffedPositionsAsync(DateTime RequestDate)
        {
            try
            {
                var Client = Factory.CreateClient();
                var Content = new FormUrlEncodedContent(Requests.AllSchedulesByDate(RequestDate));

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
        public async Task<Stream> StreamStaffedPositionsAsync(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var Client = Factory.CreateClient();
                var Content = new FormUrlEncodedContent(Requests.AllSchedulesByDateRange(StartDate, EndDate));

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
        public async Task<Stream> StreamStaffedPositionsAsync(int Month, int Year)
        {
            try
            {
                var Client = Factory.CreateClient();
                var Content = new FormUrlEncodedContent(Requests.AllSchedulesByMonth(new DateTime(Year, Month, 1)));

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

        public async Task<IList<FireManagerStaffedPosition>> GetStaffedPositionsAsync(DateTime RequestDate)
        {
            IList<FireManagerStaffedPosition> FireManagerStaffedPositions = new List<FireManagerStaffedPosition>();
            var Serializer = new XmlSerializer(typeof(Results));

            using var xReader = XmlReader.Create(await StreamStaffedPositionsAsync(RequestDate));
            var Results = (Results)Serializer.Deserialize(xReader);

            foreach (var range in Results.ResultsRanges.Range)
                FireManagerStaffedPositions.Add(
                    FireManagerStaffedPosition.Instance(
                        schedule: range.Schedule,
                        position: range.Position,
                        member: range.Member,
                        begin: range.Begin,
                        end: range.End));

            return FireManagerStaffedPositions;
        }
        public async Task<IList<FireManagerStaffedPosition>> GetStaffedPositionsAsync(DateTime StartDate, DateTime EndDate)
        {
            IList<FireManagerStaffedPosition> FireManagerStaffedPositions = new List<FireManagerStaffedPosition>();
            var Serializer = new XmlSerializer(typeof(Results));

            using var xReader = XmlReader.Create(await StreamStaffedPositionsAsync(StartDate, EndDate));
            var Results = (Results)Serializer.Deserialize(xReader);

            foreach (var range in Results.ResultsRanges.Range)
                FireManagerStaffedPositions.Add(
                    FireManagerStaffedPosition.Instance(
                        schedule: range.Schedule,
                        position: range.Position,
                        member: range.Member,
                        begin: range.Begin,
                        end: range.End));

            return FireManagerStaffedPositions;
        }
        public async Task<IList<FireManagerStaffedPosition>> GetStaffedPositionsAsync(int Month, int Year)
        {
            IList<FireManagerStaffedPosition> FireManagerStaffedPositions = new List<FireManagerStaffedPosition>();
            var Serializer = new XmlSerializer(typeof(Results));

            using var xReader = XmlReader.Create(await StreamStaffedPositionsAsync(Month, Year));
            var Results = (Results)Serializer.Deserialize(xReader);

            foreach (var range in Results.ResultsRanges.Range)
                FireManagerStaffedPositions.Add(
                    FireManagerStaffedPosition.Instance(
                        schedule: range.Schedule,
                        position: range.Position,
                        member: range.Member,
                        begin: range.Begin,
                        end: range.End));

            return FireManagerStaffedPositions;
        }
    }
}
