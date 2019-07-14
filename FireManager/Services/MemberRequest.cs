using FireManager.Concrete;
using FireManager.Entities.MemberAggregate;
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
    public class MemberRequest : IMemberRequest
    {
        private readonly IRequests Request;
        private readonly FireManagerOptions Options;
        private readonly IHttpClientFactory ClientFactory;

        public MemberRequest(
            IRequests Request,
            IHttpClientFactory ClientFactory,
            IOptions<FireManagerOptions> Options)
        {
            this.Request = Request;
            this.Options = Options.Value;
            this.ClientFactory = ClientFactory;
        }

        public async Task<Stream> StreamMembersAsync(bool ActiveOnly)
        {
            try
            {
                Console.WriteLine($"Sending fire manager request...");

                HttpRequestMessage Message = null;
                var Client = ClientFactory.CreateClient();

                if (ActiveOnly)
                    Message = CreatePostMessage(Options.Url, new FormUrlEncodedContent(Request.AllActiveMembersRequest));
                else
                    Message = CreatePostMessage(Options.Url, new FormUrlEncodedContent(Request.AllMembersRequest));


                var Response = await Client.SendAsync(Message);

                Console.WriteLine($"Waiting for a response...");

                return await Response.Content.ReadAsStreamAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Fire Manager Request Error: {ex.Message}");
                return null;
            }
        }
        public async Task<IList<FireManagerMember>> GetMembersAsync(bool IsActive)
        {
            IList<FireManagerMember> FireManagerMembers = new List<FireManagerMember>();
            var Serializer = new XmlSerializer(typeof(Results));
            using (var xReader = XmlReader.Create(await StreamMembersAsync(IsActive)))
            {
                Console.WriteLine("Streaming response...");

                var Results = (Results)Serializer.Deserialize(xReader);

                if (Results != null)
                    foreach (var Member in Results.Members.Member.ToList())
                        FireManagerMembers.Add(FireManagerMember.Instance(Member));

                Console.WriteLine("Reesponse complete...");

                return FireManagerMembers;
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