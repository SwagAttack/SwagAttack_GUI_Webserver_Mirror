using System;
using System.Collections.Generic;
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
        private IClientWrapper _clientFake;
        private IUser _userFake;

        [SetUp]
        public void Setup()
        {
            _clientFake = Substitute.For<IClientWrapper>();

            _userFake = Substitute.For<IUser>();
            _userFake.Username = "TestUserName";
            _userFake.Password = "TestPassWord";
            _userFake.GivenName = "TestMansen";
            _userFake.LastName = "Swagattack";
            _userFake.Email = "swAG@test.com";
        }

        [Test]
        public void RequestInstance_ClientReceivedCallToGetInstance()
        {
            //arrange 
            var uut = new UserProxy(_clientFake);

            //act
            var result = uut.RequestInstanceAsync(_userFake.Username, _userFake.Password);
            
            //assert

            _clientFake.Received().GetInstance();
        }

        [Test]
        public void CreateInstance_ClientReceivedCallToGetInstance()
        {
            //arrange 
            var uut = new UserProxy(_clientFake);

            //act
            var result = uut.CreateInstanceAsync(_userFake);

            //assert

            _clientFake.Received().GetInstance();
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
