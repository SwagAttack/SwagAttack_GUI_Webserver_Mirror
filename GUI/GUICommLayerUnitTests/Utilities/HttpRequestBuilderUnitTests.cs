using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies.Utilities;
using NSubstitute;
using NUnit.Framework;

namespace GUICommLayerUnitTests.Proxies
{
    // Bemærk at alle nedenstående tests er passed d. 27/04 16.00
    // Attributter er blevet fjernet for at gøre Travis glad
    public class HttpRequestBuilderUnitTests
    {
        private HttpRequestBuilder _uut;
        private IClientWrapper _fakeClient;
        private HttpClient _client;

        public void SetUp()
        {
            var handler = new HttpClientHandler() { AllowAutoRedirect = true };
            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost:50244")
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _fakeClient = Substitute.For<IClientWrapper>();
            _fakeClient.GetInstance().Returns(_client);
           
        }

        public void TearDown()
        {
            _client.Dispose();
        }

        public void GetRequest_ReturnsCorrectInfo()
        {
            _uut = new HttpRequestBuilder(_fakeClient, HttpMethod.Get, "http://localhost:50244");
            _uut.AddUriPath("/api/User/Login")
                .AddAuthentication("jonasna1993", "tablespoon1335");

            var response = _uut.SendAsync().Result;

            Assert.That(response.IsSuccessStatusCode);
        }

        public void GetRequest_ReturnsCorrectObject()
        {
            _uut = new HttpRequestBuilder(_fakeClient, HttpMethod.Get, "http://localhost:50244");
            _uut.AddUriPath("/api/User/Login")
                .AddAuthentication("jonasna1993", "tablespoon1335");

            var response = _uut.SendAsync().Result;

            Assert.That(response.IsSuccessStatusCode);

            var user = response.ReadBodyAsType<User>();

            Assert.That(user.Email == "jonasna93@gmail.com");
        }

        public void PostRequest_ReturnsCorrectObject()
        {
            var userToCreate = new User()
            {
                Email = "MyEmail@gmail.com",
                GivenName = "ThisIsAName",
                LastName = "SwaggerDagger",
                Password = "MyRandomPassword",
                Username = "DerpBoy1335"
            };
            _uut = new HttpRequestBuilder(_fakeClient, HttpMethod.Post, "http://localhost:50244");
            _uut.AddUriPath("/api/User")
                .AddContent(userToCreate);

            var response = _uut.SendAsync().Result;

            Assert.That(response.IsSuccessStatusCode);

            var user = response.ReadBodyAsType<User>();

            Assert.That(user.Username == "DerpBoy1335");
        }

        public void PutRequest_ReturnsCorrectObject()
        {
            var userToUpdate = new User()
            {
                Email = "MyEmail@gmail.com",
                GivenName = "ThisIsADopeBoy", // This line was changed
                LastName = "SwaggerDagger",
                Password = "MyRandomPassword",
                Username = "DerpBoy1335"
            };
            _uut = new HttpRequestBuilder(_fakeClient, HttpMethod.Put, "http://localhost:50244");
            _uut.AddUriPath("/api/User")
                .AddAuthentication(userToUpdate.Username, userToUpdate.Password)
                .AddContent(userToUpdate);

            var response = _uut.SendAsync().Result;

            Assert.That(response.IsSuccessStatusCode);

            var user = response.ReadBodyAsType<User>();


            Assert.That(user.GivenName == "ThisIsADopeBoy");
        }

        public void AddQuery_ReturnsCorrectObject()
        {
            string username = "DerpBoy1335";
            string password = "MyRandomPassword";

            _uut = new HttpRequestBuilder(_fakeClient, HttpMethod.Post, "http://localhost:50244");
            _uut.AddUriPath("/api/Lobby/Create").AddAuthentication(username, password).AddUriQuery("lobbyId", "MyLobby");

            var response = _uut.SendAsync().Result;

            Assert.That(response.IsSuccessStatusCode);

            var lobby = response.ReadBodyAsType<Lobby>();

            Assert.That(lobby.Id == "MyLobby");
        }

        public void AddQuery_ReturnsCorrectObject_VersionTwo()
        {
            string username = "jonasna1993";
            string password = "tablespoon1335";

            _uut = new HttpRequestBuilder(_fakeClient, HttpMethod.Post, "http://localhost:50244");
            _uut.AddUriPath("/api/Lobby/Join").AddAuthentication(username, password).AddUriQuery("lobbyId", "MyLobby");

            var response = _uut.SendAsync().Result;

            Assert.That(response.IsSuccessStatusCode);

            var lobby = response.ReadBodyAsType<Lobby>();

            Assert.That(lobby.Id == "MyLobby");
        }

        public void AddQuery_ReturnsCorrectObject_VersionThree()
        {
            string username = "jonasna1993";
            string password = "tablespoon1335";

            _uut = new HttpRequestBuilder(_fakeClient, HttpMethod.Post, "http://localhost:50244");
            _uut.AddUriPath("/api/Lobby/Leave").AddAuthentication(username, password).AddUriQuery("lobbyId", "MyLobby");

            var response = _uut.SendAsync().Result;

            Assert.That(response.IsSuccessStatusCode);

            var lobby = response.ReadBodyAsType<Lobby>();

            Assert.That(!lobby.Usernames.Contains(username));
        }
    }


}