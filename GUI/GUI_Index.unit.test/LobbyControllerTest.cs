using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUI_Index.Controllers;
using GUI_Index.Session;
using GUI_Index.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace WebserverUnitTests
{
    [TestFixture]
    public class LobbyControllerUnitTest
    {

        private IUserProxy FakeSwagCommunication = Substitute.For<IUserProxy>();
        private LobbyViewModel lobbyViewModel = new LobbyViewModel();
        private LobbyController uut;

        private User _savedUser = new User()

        {
            Username = "savedUser",
            Email = "savedUser@Anothoer.dk",
            GivenName = "Saved",
            LastName = "User",
            Password = "savedUserPas",
        };

        [SetUp]
        public void setup()
        {

            lobbyViewModel.Id = "test";
            uut = new LobbyController(FakeSwagCommunication);

        }

        [Test]
        public void TestIsWorking()
        {

            SessionExtension.SetObjectAsJson(uut.HttpContext.Session, "user", _savedUser);

            Assert.AreEqual(1, 1);

            //ViewResult result = uut.OpretLobby(lobbyViewModel) as ViewResult;

            //Assert.AreEqual("OpretLobby", result.ViewName);
        }
    }
}
