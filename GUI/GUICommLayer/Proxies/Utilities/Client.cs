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
        private static System.Net.Http.HttpClient _httpClient;

        private Uri _uri = new Uri("https://swagattackapi.azurewebsites.net/");

        public HttpClient getInstance()
        {
            if (_httpClient == null)
            {
                _httpClient = new System.Net.Http.HttpClient
                {
                    BaseAddress = _uri
                };
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            }

            return _httpClient;
        }
    }
}
