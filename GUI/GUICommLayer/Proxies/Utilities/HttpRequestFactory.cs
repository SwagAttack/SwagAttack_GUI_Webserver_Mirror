using System;
using System.Net.Http;
using Newtonsoft.Json;
using GUICommLayer.Interfaces;

namespace GUICommLayer.Proxies.Utilities
{
    public class HttpRequestFactory : IHttpRequestFactory
    {
        public IClientWrapper Client { get; }
        public string BaseAddress { get; }

        public HttpRequestFactory(IClientWrapper client, string baseAddress)
        {
            BaseAddress = baseAddress ?? throw new ArgumentNullException(nameof(baseAddress));
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public IHttpRequestBuilder Get(string path)
        {
            var builder = new HttpRequestBuilder(Client, HttpMethod.Get, BaseAddress);
            builder.AddUriPath(path);
            return builder;
        }

        public IHttpRequestBuilder Post(string path, object obj = null)
        {
            var builder = new HttpRequestBuilder(Client, HttpMethod.Post, BaseAddress)
                .AddUriPath(path);

            if (obj != null)
                builder.AddContent(obj);

            return builder;
        }

        public IHttpRequestBuilder Put(string path, object obj = null)
        {
            var builder = new HttpRequestBuilder(Client, HttpMethod.Put, BaseAddress)
                .AddUriPath(path);

            if (obj != null)
                builder.AddContent(obj);

            return builder;
        }

        public IHttpRequestBuilder Delete(string path)
        {
            var builder = new HttpRequestBuilder(Client, HttpMethod.Delete, BaseAddress)
                .AddUriPath(path);
            return builder;
        }
    }
}