using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using GUICommLayer.Interfaces;

namespace GUICommLayer.Proxies.Utilities
{
    public class Client : IHttpClient
    {
        private static System.Net.Http.HttpClient _httpClient;

        private Uri _uri = new Uri("https://swagattackapi.azurewebsites.net/");
 

        System.Net.Http.HttpClient IHttpClient.getInstance()
        {
            return GetInstance();
        }

        public static System.Net.Http.HttpClient GetInstance()
        {
            if (_httpClient == null)
            {
                _httpClient = new System.Net.Http.HttpClient();

            }

            return _httpClient;
        }

        private Client()
        {
            _httpClient.BaseAddress = _uri;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
