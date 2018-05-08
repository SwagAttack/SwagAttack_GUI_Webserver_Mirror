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
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
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
        public void HomeControllerLogInd_ViewNameCorrect()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(
                @"C:\Users\maxbj\Documents\GitHub\SwagAttack_GUI_Webserver_Mirror\GUI\WebserverIntegrationTests\bin\Debug\netcoreapp2.0", "geckodriver.exe");

            FirefoxProfile profile = new FirefoxProfile();

            var op = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };



            service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";



            System.Environment.SetEnvironmentVariable("webdriver.gecko.driver", @"C:\Users\maxbj\Documents\GitHub\SwagAttack_GUI_Webserver_Mirror\GUI\WebserverIntegrationTests\bin\Debug\netcoreapp2.0");

            IWebDriver driver = new FirefoxDriver(service, op, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("https://localhost:44321/");

            IWebElement typeUser = driver.FindElement(By.Name("Username"));
            IWebElement typePass = driver.FindElement(By.Name("Password"));

            typeUser.SendKeys("apiuserh");
            typePass.SendKeys("Maxmaxmax");

            driver.FindElement(By.XPath("//button[1]")).Click();

            driver.Quit();

            _fakeUserProxy.Received(1).RequestInstanceAsync(Arg.Any<string>(), Arg.Any<string>());

        }


    }




}
