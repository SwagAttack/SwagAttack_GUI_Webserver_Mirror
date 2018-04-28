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
        private ILobbyProxy FakeSwagLobby = Substitute.For<ILobbyProxy>();
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
            _lobbyViewModel.Admin = _savedUser.Username;
            _lobbyViewModel.Usernames.Add(_savedUser.Username);

        }

        [Test]
        public void OpretLobby_PostRedirectToAction_Redirects()
        {            
            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);
            var sut = new LobbyController(FakeSwagCommunication,FakeSwagLobby,mockUserSession.Object);

            // Act
            var result = sut.OpretLobby(_lobbyViewModel);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public void OpretLobby_PostRedirectToAction_CorrectViewModel()
        {
            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);
            var sut = new LobbyController(FakeSwagCommunication,FakeSwagLobby, mockUserSession.Object);

            // Act
            var result = sut.OpretLobby(_lobbyViewModel) as ViewResult;

            // Assert
            Assert.IsInstanceOf<LobbyViewModel>(result.Model);
        }

        [Test]
        public void OpretLobby_GoToOpretLobby_ReturnsCorrectViewName()
        {

            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);
            var sut = new LobbyController(FakeSwagCommunication,FakeSwagLobby, mockUserSession.Object);

            // Act
            var result = sut.OpretLobby() as ViewResult;

            // Assert
            Assert.AreEqual("OpretLobby", result.ViewName);
        }

        ////[Test]
        ////public void TilslutLobby_GotoTilslutLobby_ReturnsCorrectViewModel()
        ////{
        ////    // Arrange
        ////    var mockUserSession = new Mock<IUserSession>();
        ////    mockUserSession.Setup(x => x.User).Returns(_savedUser);
        ////    var sut = new LobbyController(FakeSwagCommunication,FakeSwagLobby, mockUserSession.Object);

        ////    // Act
        ////    var result = sut.TilslutLobby() as ViewResult;

        ////    // Assert
        ////    Assert.IsInstanceOf<TilslutLobbyViewModel>(result.Model);

        ////}

    }
}
