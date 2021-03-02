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
    internal class PositionRequest : RequestBase, IPositionRequest
    {
        public PositionRequest(
            IRequests Requests,
            IHttpClientFactory Factory,
            IOptions<FireManagerOptions> Options) : base(Requests, Factory, Options)
        { }

        public async Task<Stream> StreamPositionsAsync()
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
        public async IAsyncEnumerable<FireManagerPosition> GetPositionsAsync()
        {
            var Serializer = new XmlSerializer(typeof(Results));

            using var xReader = XmlReader.Create(await StreamPositionsAsync());
            var Results = (Results)Serializer.Deserialize(xReader);

            if (Results != null)
                foreach (var Schedule in Results.Schedules.Schedule.ToList())
                    foreach (var Position in Schedule.Positions.ToList())
                        foreach (var item in Position.Position.ToList())
                            yield return FireManagerPosition.Instance(Schedule, item);
        }
    }
}