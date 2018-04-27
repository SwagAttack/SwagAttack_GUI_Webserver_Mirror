using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Domain.Interfaces;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies;
using GUICommLayer.Proxies.Utilities;
using NSubstitute;
using NUnit.Framework;

namespace GUICommLayerUnitTests.Proxies
{
    [TestFixture]
    public class UserProxyUnitTest
    {
        private IHttpRequestFactory _fakeFactory;
        private IHttpRequestBuilder _fakeBuilder;
        private IUser _userFake;

        [SetUp]
        public void Setup()
        {
            _fakeFactory = Substitute.For<IHttpRequestFactory>();
            _fakeBuilder = Substitute.For<IHttpRequestBuilder>();
            _fakeBuilder.SendAsync().Returns(new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest });
            _fakeBuilder.AddAuthentication(Arg.Any<string>(), Arg.Any<string>()).Returns(_fakeBuilder);

            _userFake = Substitute.For<IUser>();
            _userFake.Username = "TestUserName";
            _userFake.Password = "TestPassWord";
            _userFake.GivenName = "TestMansen";
            _userFake.LastName = "Swagattack";
            _userFake.Email = "swAG@test.com";
        }

        [Test]
        public void RequestInstance_CallsGetWithCorrectPath()
        {
            //arrange 
            var uut = new UserProxy(_fakeFactory);
            _fakeFactory.Get("api/User/Login").Returns(_fakeBuilder);

            //act
            var result = uut.RequestInstanceAsync(_userFake.Username, _userFake.Password).Result;

            //assert
            _fakeFactory.Received().Get("api/User/Login");
            
        }

        [Test]
        public void RequestInstance_CallsSendOnBuilder()
        {
            //arrange 
            var uut = new UserProxy(_fakeFactory);
            _fakeFactory.Get("api/User/Login").Returns(_fakeBuilder);
            _fakeBuilder.SendAsync().Returns(new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest });

            //act
            var result = uut.RequestInstanceAsync(_userFake.Username, _userFake.Password).Result;

            //assert
            _fakeBuilder.Received(1).SendAsync();

        }

        [Test]
        public void CreateInstance_CallsPostWithCorrectPath()
        {
            //arrange 
            var uut = new UserProxy(_fakeFactory);
            _fakeFactory.Post(Arg.Any<string>(), _userFake).Returns(_fakeBuilder);
            _fakeBuilder.SendAsync().Returns(new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest });

            //act
            var result = uut.CreateInstanceAsync(_userFake).Result;

            //assert
            _fakeFactory.Received(1).Post("api/User", _userFake);
        }

        [Test]
        public void CreateInstance_CallsSendOnBuilder()
        {
            //arrange 
            var uut = new UserProxy(_fakeFactory);
            _fakeFactory.Post("api/User", _userFake).Returns(_fakeBuilder);
            _fakeBuilder.SendAsync().Returns(new HttpResponseMessage() {StatusCode = HttpStatusCode.BadRequest});

            //act
            var result = uut.CreateInstanceAsync(_userFake).Result;

            //assert
            _fakeBuilder.Received(1).SendAsync();
        }

        #region How to test this in unittest ? mock api i webserver -> dette vil sige navngivning er anderledes? 
        /*
        [Test]
        public void RequestInstance_ReturnsCorrectUser()
        {

        }

        [Test]
        public void RequestInstance_ReturnsNull()
        {

        }

        [Test]
        public void CreateInstance_ReturnsCorrectUser()
        {

        }

        [Test]
        public void CreateInstance_ReturnsNull()
        {

        }
        */
        #endregion
    }
}
