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
using static System.Console;

namespace FireManager.Services
{
    internal class MemberRequest : RequestBase, IMemberRequest
    {
        public MemberRequest(
            IRequests Request,
            IHttpClientFactory Factory,
            IOptions<FireManagerOptions> Options) : base(Request, Factory, Options)
        { }

        public async Task<Stream> StreamMembersAsync(bool ActiveOnly)
        {
            try
            {
                WriteLine($"Sending fire manager request...");

                HttpRequestMessage Message = null;
                var Client = Factory.CreateClient();

                if (ActiveOnly)
                    Message = CreatePostMessage(Options.Url, new FormUrlEncodedContent(Requests.AllActiveMembersRequest));
                else
                    Message = CreatePostMessage(Options.Url, new FormUrlEncodedContent(Requests.AllMembersRequest));

                var Response = await Client.SendAsync(Message);

                WriteLine($"Waiting for a response...");

                return await Response.Content.ReadAsStreamAsync();
            }
            catch (Exception ex)
            {
                WriteLine($"Fire Manager Request Error: {ex.Message}");
                return null;
            }
        }
        public async IAsyncEnumerable<FireManagerMember> GetMembersAsync(bool IsActive)
        {
            var Serializer = new XmlSerializer(typeof(Results));
            using var xReader = XmlReader.Create(await StreamMembersAsync(IsActive));

            WriteLine("Streaming response...");

            var Results = (Results)Serializer.Deserialize(xReader);

            if (Results != null)
                foreach (var Member in Results.Members.Member.ToList())
                    yield return Member;

            WriteLine("Reesponse complete...");
        }
    }
}