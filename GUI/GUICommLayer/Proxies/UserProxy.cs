using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using GUICommLayer.Interfaces;
using Newtonsoft.Json.Linq;

namespace GUICommLayer.Proxies
{
    public class UserProxy : IUserProxy
    {
        private static IClientWrapper _client;

        public UserProxy(IClientWrapper client)
        {
            _client = client;
        }
        
        public static async Task<IUser> CreateInstance(IUser user)
        {
            var jsonObject = Utilities.Utility.ComposeJson(null, null, user);

            var httpContent = new StringContent(jsonObject.ToString());
            
            HttpResponseMessage response = await _client.GetInstance().PostAsync("api/User", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<User>();
            }

            return null;
        }

        public static async Task<IUser> RequestInstance(string username, string password )
        {
            var jsonObject = Utilities.Utility.ComposeJson<IUser>(username, password, null);

            var httpContent = new StringContent(jsonObject.ToString());

            HttpResponseMessage response = await _client.GetInstance().PostAsync("api/User/Login", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<User>();
            }

            return null;
        }

        public async Task<IUser> CreateInstanceAsync(IUser user)
        {
            return await CreateInstance(user);
        }

        public async Task<IUser> RequestInstanceAsync(string username, string password)
        {
            return await RequestInstance(username, password);
        }
        
    }
}