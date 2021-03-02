using FireManager.Extensions;
using FireManager.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace FireManager.Abstract
{
    public abstract class RequestBase
    {
        protected readonly IRequests Requests;
        protected readonly IHttpClientFactory Factory;
        protected readonly FireManagerOptions Options;

        protected RequestBase(
            IRequests Requests,
            IHttpClientFactory Factory,
            IOptions<FireManagerOptions> Options)
        {
            this.Requests = Requests;
            this.Options = Options.Value;
            this.Factory = Factory;
        }

        public static HttpRequestMessage CreatePostMessage(string Path, FormUrlEncodedContent Content)
        {
            return new HttpRequestMessage()
            {
                RequestUri = new Uri(Path),
                Method = HttpMethod.Post,
                Content = Content
            };
        }
    }

    public abstract class RequestBase<T> : RequestBase
    {
        protected RequestBase(
            IRequests Requests,
            IHttpClientFactory Factory,
            IOptions<FireManagerOptions> Options) : base(Requests, Factory, Options)
        { }
    }
}
