using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace GUICommLayer.Proxies.Utilities
{
    public static class Utility
    {
        public static JObject ComposeJson<T>(string username, string password, T obj)
        {
            var jobject = new JObject();
            Dictionary<string,string> dict = null;
            if (username != null && password != null)
            {
                dict = new Dictionary<string, string> {{"Username", username}, {"Password", password}};

                jobject.Add("auth", JObject.FromObject(dict));
            }

            if (obj != null)
                jobject.Add("val", JObject.FromObject(obj));
            else
                jobject.Add("val", JObject.FromObject(dict));

            return jobject;
        }
    }
}