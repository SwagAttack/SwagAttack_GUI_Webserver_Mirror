using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GUICommLayer;
using Models.Interfaces;
using Models.User;
//strongly inspired by https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

namespace GUI_Index
{
    public class SwagCommunication : ISwagCommunication
    {
        private static readonly HttpClient _client = new HttpClient();

        public static HttpClient Client => _client;

        private static string ApiUsers = "api/User/";

        /// <summary>
        /// Ctor for swagCommunication
        /// </summary>
        /// <param name="uri">Uri with port like: "http://localhost:50244/"</param>
        public SwagCommunication(string uri)
        {
            _client.BaseAddress = new Uri(uri);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<Uri> CreateUserAsync(IUser user)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(
                "api/User", user);
            //response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            return response.Headers.Location;
        }
        /// <summary>
        /// Get user async
        /// Null if not succes.
        /// </summary>
        /// <param name="username"> Username of user to get</param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(string username, string password)
        {
            string path = ApiUsers + username + "/" + password;


            HttpResponseMessage respondHttpResponseMessage = await _client.GetAsync(path);

            //Set user to respond if responds seuccesfully recieved. 
            if (respondHttpResponseMessage.IsSuccessStatusCode)
            {
                User user = await respondHttpResponseMessage.Content.ReadAsAsync<User>();
                return user;
            }
            else
            {
                return default(User);
            }

            //return user that will be null if nothing recived
        }


        public async Task<HttpStatusCode> DeleteProductAsync(string username)
        {
            HttpResponseMessage response = await _client.DeleteAsync(
                ApiUsers + username);

            return response.StatusCode;
        }


    }


}
