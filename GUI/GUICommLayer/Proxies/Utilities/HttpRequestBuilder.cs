using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GUICommLayer.Interfaces;
using Newtonsoft.Json;

namespace GUICommLayer.Proxies.Utilities
{
    /// <summary>
    /// Inspired by:
    /// https://github.com/TahirNaushad/Fiver.Api.HttpClient/blob/master/Fiver.Api.HttpClient/HttpRequestBuilder.cs
    /// </summary>
    public class HttpRequestBuilder : IHttpRequestBuilder
    {
        private static string _usernameCredentials = "username";
        private static string _passwordCredentials = "password";

        private readonly IClientWrapper _client;
        private readonly HttpMethod _method = null;
        private string _requestUriPath = "";
        private string _requestUriBase = "";
        private string _uriQuery = "";
        private HttpContent _content = null;
        private readonly Dictionary<string, string> _authenticationDictionary;

        public HttpMethod Method => _method;
        public string RequestUriPath => _requestUriPath;
        public string UriQuery => _uriQuery;
        public HttpContent Content => _content;
        public Dictionary<string, string> AuthenticationDictionary => _authenticationDictionary;

        public HttpRequestBuilder(IClientWrapper client, HttpMethod method, string baseUri)
        {
            _method = method ?? throw new ArgumentNullException(nameof(method));
            _requestUriBase = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            _client = client;
            _authenticationDictionary = new Dictionary<string, string>();
        }

        public IHttpRequestBuilder AddUriPath(string path)
        {
            _requestUriPath = path;
            return this;
        }

        public IHttpRequestBuilder AddContent(object content)
        {
            _content = new JsonContent(content);
            return this;
        }

        public IHttpRequestBuilder AddUriQuery(string name, string value)
        {
            var builder = new UriBuilder { Query = _uriQuery };
            var query = HttpUtility.ParseQueryString(builder.Query);
            query[name] = value;
            builder.Query = query.ToString();
            _uriQuery = builder.Query;
            return this;
        }

        public IHttpRequestBuilder AddAuthentication(string username, string password)
        {
            _authenticationDictionary[_usernameCredentials] = username;
            _authenticationDictionary[_passwordCredentials] = password;
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            // If request uri is null throw
            if (string.IsNullOrEmpty(_requestUriPath))
                throw new ArgumentNullException(nameof(_requestUriPath));

            // Set up request
            var request = new HttpRequestMessage
            {
                Method = _method,
                RequestUri = new Uri(_requestUriBase + _requestUriPath + _uriQuery),
            };

            if (_content != null)
                request.Content = _content;

            request.Headers.Accept.Clear();
            if (_authenticationDictionary.Count == 2)
            {
                request.Headers.Add(_usernameCredentials, _authenticationDictionary[_usernameCredentials]);
                request.Headers.Add(_passwordCredentials, _authenticationDictionary[_passwordCredentials]);
            }

            var client = _client.GetInstance();
            return await client.SendAsync(request);
        }

        #region Utility

        private class JsonContent : StringContent
        {
            public JsonContent(object obj)
                : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            { }
        }

        #endregion
    }
}