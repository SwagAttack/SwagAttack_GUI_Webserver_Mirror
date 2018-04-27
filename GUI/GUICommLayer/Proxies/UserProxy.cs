using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies.Utilities;
using Newtonsoft.Json.Linq;

namespace GUICommLayer.Proxies
{
    public class UserProxy : IUserProxy
    {
        private readonly IHttpRequestFactory _httpRequestFactory;

        public UserProxy(IHttpRequestFactory httpRequestFactory)
        {
            _httpRequestFactory = httpRequestFactory;
        }
        
        public async Task<IUser> CreateInstanceAsync(IUser user)
        {
            var request = _httpRequestFactory.Post("api/User", user);
            var response = await request.SendAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return response.ReadBodyAsType<User>();           
        }

        public async Task<IUser> RequestInstanceAsync(string username, string password)
        {
            var request = _httpRequestFactory.Get("api/User/Login").AddAuthentication(username, password);
            var response = await request.SendAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return response.ReadBodyAsType<User>();
        }
        
    }
}