using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using GUICommLayer.Interfaces;

namespace GUICommLayer.Proxies.Utilities
{
    public class Client : IClientWrapper
    {
        private static HttpClient _httpClient;

        public HttpClient GetInstance()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            }

            return _httpClient;
        }
    }
}
