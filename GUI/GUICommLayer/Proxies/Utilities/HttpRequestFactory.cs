using System;
using System.Net.Http;
using Newtonsoft.Json;
using GUICommLayer.Interfaces;

namespace GUICommLayer.Proxies.Utilities
{
    public class HttpRequestFactory : IHttpRequestFactory
    {
        public static IClientWrapper Client { get; set; }

        public HttpRequestFactory(IClientWrapper client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public HttpRequestBuilder Get(string uri)
        {
            var builder = new HttpRequestBuilder(Client)
                .AddMethod(HttpMethod.Get)
                .AddRequestUri(uri);

            return builder;
        }

        public HttpRequestBuilder Post(string uri, object obj = null)
        {
            var builder = new HttpRequestBuilder(Client)
                .AddMethod(HttpMethod.Post)
                .AddRequestUri(uri);

            if (obj != null)
                builder.AddContent(obj);

            return builder;
        }

        public HttpRequestBuilder Put(string uri, object obj = null)
        {
            var builder = new HttpRequestBuilder(Client)
                .AddMethod(HttpMethod.Put)
                .AddRequestUri(uri);

            if (obj != null)
                builder.AddContent(obj);

            return builder;
        }

        public HttpRequestBuilder Delete(string uri)
        {
            var builder = new HttpRequestBuilder(Client)
                .AddMethod(HttpMethod.Delete)
                .AddRequestUri(uri);

            return builder;
        }
    }
}