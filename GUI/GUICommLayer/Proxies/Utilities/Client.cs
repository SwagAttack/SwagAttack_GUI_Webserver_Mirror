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

        public HttpClient GetInstance(string uri = null)
        {
            if (_httpClient == null)
            {
                if(uri != null) _uri = new Uri(uri);
                var handler = new HttpClientHandler(){ AllowAutoRedirect = true };
                _httpClient = new HttpClient(handler)
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
