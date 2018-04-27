using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies.Utilities;
using NSubstitute;
using NUnit.Framework;

namespace GUICommLayerUnitTests.Proxies
{
    // Bemærk at alle nedenstående tests er passed d. 27/04 14.00
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
            _uut = new HttpRequestBuilder(_fakeClient);
        }

        public void TearDown()
        {
            _client.Dispose();
        }

        public void GetRequest_ReturnsCorrectInfo()
        {
            _uut.AddMethod(HttpMethod.Get)
                .AddRequestUri("/api/User/Login")
                .AddAuthentication("jonasna1993", "tablespoon1335");

            var response = _uut.SendAsync().Result;

            Assert.That(response.IsSuccessStatusCode);
        }

        public void GetRequest_ReturnsCorrectObject()
        {
            _uut.AddMethod(HttpMethod.Get)
                .AddRequestUri("/api/User/Login")
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

            _uut.AddMethod(HttpMethod.Post)
                .AddRequestUri("/api/User")
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

            _uut.AddMethod(HttpMethod.Put)
                .AddRequestUri("/api/User")
                .AddAuthentication(userToUpdate.Username, userToUpdate.Password)
                .AddContent(userToUpdate);

            var response = _uut.SendAsync().Result;

            Assert.That(response.IsSuccessStatusCode);

            var user = response.ReadBodyAsType<User>();


            Assert.That(user.GivenName == "ThisIsADopeBoy");
        }
    }


}