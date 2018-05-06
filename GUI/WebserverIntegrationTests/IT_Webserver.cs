using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Castle.Core.Configuration;
using Domain.Interfaces;
using Domain.Misc;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies;
using GUICommLayer.Proxies.Utilities;
using GUI_Index;
using GUI_Index.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using GUI_Index.Interfaces;
using GUI_Index.Session;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Rest;
using MvcIntegrationTestFramework.Browsing;
using MvcIntegrationTestFramework.Hosting;
using RestEase;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace WebserverIntegrationTests
{

   


    public class IntegrationTest_ServerProvider
    {

        public static IUserProxy FakeUserProxy = Substitute.For<IUserProxy>();
        public static ILobbyProxy FakeLobbyProxy = Substitute.For<ILobbyProxy>();
        public static IUserSession FakeUserSession = Substitute.For<IUserSession>();
        public static IHttpContextAccessor FakeAccessor = Substitute.For<IHttpContextAccessor>();
        public static IHttpRequestFactory FakeFactory = Substitute.For<IHttpRequestFactory>();

        

        public IntegrationTest_ServerProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IUserProxy>(provider => FakeUserProxy);
            services.AddTransient<ILobbyProxy>(provider => FakeLobbyProxy);


            services.AddScoped<IUserSession>(provider => FakeUserSession);
            services.AddSingleton<IHttpContextAccessor>(provider => FakeAccessor);

            var sp = services.BuildServiceProvider();
            services.AddTransient<IHttpRequestFactory, HttpRequestFactory>(f =>
                new HttpRequestFactory(sp.GetService<IClientWrapper>(),
                    "api/Users"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }



    [TestFixture]
    public class IntegrationTest1
    {
        private static TestServer _server;
        private static HttpClient _client;
        private readonly User user = new User();
        private readonly User atrrUser = new User();

        private IHomeController _fakeHomeController;
        private IHttpRequestFactory _fakeHttpRequestFactory;
        private IHttpRequestBuilder _fakeHttpRequestBuilder;
        private IUserProxy _fakeUserProxy;
        private IHttpRequestFactory _fakeFactory;
        private IClientWrapper _fakeClientWrapper;
        private readonly IUser _person = Substitute.For<IUser>();


        //public interface IUsersApi
        //{
        //    [Get("/api/User")]
        //    Task<IEnumerable<User>> GetUserAsync(ushort mFilter);
        //}

        [SetUp]
        public void Setup()
        {
            // setup testserver
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _server.Host.Start();
            _client = _server.CreateClient();

            //TestUser
            user.Username = "apiuserh";
            user.Password = "Maxmaxmax";
            user.Email = "apiuser@123.dk";
            user.GivenName = "Apiuser";
            user.LastName = "Apiuser";


            //TestPersUI
            _person.Username = "apiuserh";
            _person.Password = "Maxmaxmax";
            _person.Email = "apiuser@123.dk";
            _person.GivenName = "Apiuser";
            _person.LastName = "Apiuser";

            //Setup Homecontroller

            _fakeHomeController = Substitute.For<IHomeController>();
            _fakeHttpRequestFactory = Substitute.For<IHttpRequestFactory>();
            _fakeHttpRequestBuilder = Substitute.For<IHttpRequestBuilder>();
            _fakeUserProxy = Substitute.For<IUserProxy>();
            _fakeHttpRequestFactory = Substitute.For<IHttpRequestFactory>();


            IntegrationTest_ServerProvider.FakeUserProxy.CreateInstanceAsync(_person);
            IntegrationTest_ServerProvider.FakeFactory.Post(Arg.Any<string>(), Arg.Any<object>());
            _fakeUserProxy = IntegrationTest_ServerProvider.FakeUserProxy;
            _fakeHttpRequestFactory = IntegrationTest_ServerProvider.FakeFactory;
        }


        //[Produces("application/json")]
        //[Route("api/User")]
        //[HttpGet("Login", Name = "GetUser")]
        //public User GetUser([FromHeader] string username, [FromHeader] string password)
        //{
        //    atrrUser.Username = username;
        //    atrrUser.Password = password;
        //    return (atrrUser);
        //}

        

        //Test if user was logged in
        [Test]
        public async Task UsersApi_LoginUser()
        {                   
            ////act
            var sendUser = _fakeUserProxy.RequestInstanceAsync(user.Username, user.Password);
            sendUser.Wait();

            //var request = "api/User/Login";
            //var response = await _client.GetAsync(request);

            
            ////assert
            Assert.That(sendUser.Result, Is.Not.Null);
        }


        //Test CreateUser

        [Test]
        public async Task UsersApi_Create_User()
        {
            ////act

            var sendUser = _fakeUserProxy.CreateInstanceAsync(user);
            sendUser.Wait();

            ////assert


            Assert.AreEqual(sendUser.Result, Is.Null);
        }


        //[Test]
        //public async Task Server_Recived()
        //{
        //    ////act

        //    var recived = _server.CreateHandler().
        //    sendUser.Wait();

        //    ////assert


        //    Assert.AreEqual(sendUser.Result, Is.Null);
        //}


        [Test]
         public async Task LogInProcess()
        {
            var sendUser = _fakeUserProxy.CreateInstanceAsync(user);
            sendUser.Wait();

            var response = _client.GetAsync("api/User/Login" + user);


            var request = _fakeFactory.Get("api/User/Login").AddAuthentication(_person.Username, _person.Password);
            var outputModel = response.Result.Content.AsString();
            ////assert


            Assert.True(outputModel.Contains(Arg.Any<string>()));
        }


    }




}
