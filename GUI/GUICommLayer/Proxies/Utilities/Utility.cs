using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace GUICommLayer.Proxies.Utilities
{
    public static class Utility
    {
        public static JObject ComposeJson<T>(string username, string password, T obj)
        {
            var jobject = new JObject();
            if (username != null && password != null)
            {
                var dict = new Dictionary<string, string> {{"Username", username}, {"Password", password}};

                jobject.Add("auth", JObject.FromObject(dict));
            }

            jobject.Add("val", JObject.FromObject(obj));

            return jobject;
        }
    }
}