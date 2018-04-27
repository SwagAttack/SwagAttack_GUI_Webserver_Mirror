using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace GUICommLayer.Proxies.Utilities
{
    public static class HttpResponseExtensions
    {
        public static T ReadBodyAsType<T>(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return string.IsNullOrEmpty(data) ? default(T) : JsonConvert.DeserializeObject<T>(data);
        }

        public static object ReadBodyAsType(this HttpResponseMessage response, Type type)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return string.IsNullOrEmpty(data) ? null : JsonConvert.DeserializeObject(data, type);
        }

        public static string ReadBodyAsString(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}