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
using Moq;
using NSubstitute;
using NUnit.Framework;

namespace WebserverUnitTests
{
    [TestFixture]
    public class LobbyControllerUnitTest
    {

        private IUserProxy FakeSwagCommunication = Substitute.For<IUserProxy>();
        private LobbyViewModel _lobbyViewModel = new LobbyViewModel();
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

            _lobbyViewModel.Id = "test";
            
        }

        [Test]
        public void OpretLobby_PostRedirectToAction()
        {            
            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);
            var sut = new LobbyController(FakeSwagCommunication,mockUserSession.Object);

            // Act
            var result = sut.OpretLobby(_lobbyViewModel);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public void OpretLobby_GoToOpretLobby()
        {

            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);
            var sut = new LobbyController(FakeSwagCommunication, mockUserSession.Object);

            // Act
            var result = sut.OpretLobby() as ViewResult;

            // Assert
            Assert.AreEqual("OpretLobby", result.ViewName);
        }

        [Test]
        public void TilslutLobby_GotoTilslutLobby()
        {
            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);
            var sut = new LobbyController(FakeSwagCommunication, mockUserSession.Object);

            // Act
            var result = sut.TilslutLobby() as ViewResult;

            // Assert
            Assert.AreEqual("Tilslut Lobby", result.ViewName);
        }

    }
}
