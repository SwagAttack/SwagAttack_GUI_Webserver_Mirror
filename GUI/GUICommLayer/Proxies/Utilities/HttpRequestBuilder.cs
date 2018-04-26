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
    public class HttpRequestBuilder
    {
        private static string _usernameCredentials = "username";
        private static string _passwordCredentials = "password";

        private IClientWrapper _client;
        private HttpMethod _method = null;
        private string _requestUri = "";
        private string _uriQuery = "";
        private HttpContent _content = null;
        private readonly Dictionary<string, string> _authenticationDictionary;

        public HttpMethod Method => _method;
        public string RequestUri => _requestUri;
        public string UriQuery => _uriQuery;
        public HttpContent Content => _content;
        public Dictionary<string, string> AuthenticationDictionary => _authenticationDictionary;

        public HttpRequestBuilder(IClientWrapper client)
        {
            _client = client;
            _authenticationDictionary = new Dictionary<string, string>();
        }

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            _method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            _requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(object content)
        {
            _content = new JsonContent(content);
            return this;
        }

        public HttpRequestBuilder AddUriQuery(string name, string value)
        {
            var builder = new UriBuilder { Query = _uriQuery };
            var query = HttpUtility.ParseQueryString(builder.Query);
            query[name] = value;
            builder.Query = query.ToString();
            _uriQuery += builder.Query;
            return this;
        }

        public HttpRequestBuilder AddAuthentication(string username, string password)
        {
            _authenticationDictionary[_usernameCredentials] = username;
            _authenticationDictionary[_passwordCredentials] = password;
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            // Check required arguments
            EnsureArguments();

            // Set up request
            var request = new HttpRequestMessage
            {
                Method = _method,
                RequestUri = new Uri(_requestUri + _uriQuery)
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

        private void EnsureArguments()
        {
            if (_method == null)
                throw new ArgumentNullException(nameof(_method));

            if (string.IsNullOrEmpty(_requestUri))
                throw new ArgumentNullException(nameof(_requestUri));
        }

        private class JsonContent : StringContent
        {
            public JsonContent(object obj)
                : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            { }
        }

        #endregion
    }
}