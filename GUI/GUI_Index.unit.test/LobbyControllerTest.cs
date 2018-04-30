using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies;
using GUICommLayer.Proxies.Utilities;
using GUI_Index.Controllers;
using GUI_Index.Session;
using GUI_Index.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

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
        public void OpretLobby_GoToOpretLobby_ReturnsCorrectViewName()
        {

            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);
            var sut = new LobbyController(FakeSwagCommunication, FakeSwagLobby, mockUserSession.Object);

            // Act
            var result = sut.OpretLobby() as ViewResult;

            // Assert
            Assert.AreEqual("OpretLobby", result.ViewName);
        }

        [Test]
        public void OpretLobby_PostRedirectToActionUserLogged_on_RedirectsToLobby()
        {            

            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);

            ILobby MockedLobby = new Lobby(_savedUser.Username);
            MockedLobby.Id = _lobbyViewModel.Id;

            var mockLobbyProxy = new Mock<ILobbyProxy>();
            mockLobbyProxy
                .Setup(x => x.CreateInstanceAsync(_lobbyViewModel.Id, _savedUser.Username, _savedUser.Password))
                .Returns(Task.FromResult(MockedLobby));

            var sut = new LobbyController(FakeSwagCommunication,mockLobbyProxy.Object,mockUserSession.Object);

            // Act
            var result = (RedirectToActionResult)sut.OpretLobby(_lobbyViewModel);
            
            //Assert
            Assert.AreEqual("Lobby", result.ActionName);

        }

        [Test]
        public void OpretLobby_PostRedirectToActionUserLogged_off_RedirectsToLogin()
        {

            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);

            var mockLobbyProxy = new Mock<ILobbyProxy>();
            mockLobbyProxy
                .Setup(x => x.CreateInstanceAsync(_lobbyViewModel.Id, _savedUser.Username, _savedUser.Password))
                .Returns((Task.FromResult<ILobby>(null)));

            var sut = new LobbyController(FakeSwagCommunication, mockLobbyProxy.Object, mockUserSession.Object);

            // Act
            var result = (RedirectToActionResult)sut.OpretLobby(_lobbyViewModel);

            // Assert
            Assert.AreEqual("LogInd", result.ActionName);
        }

        [Test]
        public void TilslutLobby_UserOKreturnsLobbyView()
        {

            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);

            List<string> lobbies = new List<string>();
            lobbies.Add("test");

            var mockLobbyProxy = new Mock<ILobbyProxy>();
            mockLobbyProxy
                .Setup(x => x.GetAllLobbyIdsAsync(_savedUser.Username, _savedUser.Password))
                .ReturnsAsync(lobbies);

            var sut = new LobbyController(FakeSwagCommunication, mockLobbyProxy.Object, mockUserSession.Object);

            // Act
            var result = sut.TilslutLobby() as ViewResult;

            // Assert
            Assert.IsInstanceOf<TilslutLobbyViewModel>(result.Model);
        }

        [Test]
        public void TilslutLobby_UserNotOK_RedirectsToLogin()
        {
            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);

            var mockLobbyProxy = new Mock<ILobbyProxy>();
            mockLobbyProxy
                .Setup(x => x.GetAllLobbyIdsAsync(_savedUser.Username, _savedUser.Password))
                .ReturnsAsync((List<string>) null);

            var sut = new LobbyController(FakeSwagCommunication, mockLobbyProxy.Object, mockUserSession.Object);

            // Act
            var result = (RedirectToActionResult)sut.TilslutLobby();

            // Assert
            Assert.AreEqual("LogInd", result.ActionName);
        }

        [Test]
        public void TilslutLobby_PostRedirectToActionUserLogged_on_RedirectsToLobby()
        {
            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);

            ILobby MockedLobby = new Lobby(_savedUser.Username);
            MockedLobby.Id = _lobbyViewModel.Id;

            var mockLobbyProxy = new Mock<ILobbyProxy>();
            mockLobbyProxy
                .Setup(x => x.JoinLobbyAsync(_lobbyViewModel.Id, _savedUser.Username, _savedUser.Password))
                .Returns(Task.FromResult(MockedLobby));

            var sut = new LobbyController(FakeSwagCommunication, mockLobbyProxy.Object, mockUserSession.Object);

            // Act
            var result = (RedirectToActionResult)sut.TilslutLobby(_lobbyViewModel);

            //Assert
            Assert.AreEqual("Lobby", result.ActionName);
        }

        [Test]
        public void TilslutLobby_PostRedirectToActionUserLogged_off_RedirectsToLobby()
        {

            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);

            var mockLobbyProxy = new Mock<ILobbyProxy>();
            mockLobbyProxy
                .Setup(x => x.JoinLobbyAsync(_lobbyViewModel.Id, _savedUser.Username, _savedUser.Password))
                .Returns((Task.FromResult<ILobby>(null)));

            var sut = new LobbyController(FakeSwagCommunication, mockLobbyProxy.Object, mockUserSession.Object);

            // Act
            var result = (RedirectToActionResult)sut.TilslutLobby(_lobbyViewModel);

            // Assert
            Assert.AreEqual("TilslutLobby", result.ActionName);
        }

        [Test]
        public void Lobby_ReturnsView()
        {

            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);

            var sut = new LobbyController(FakeSwagCommunication, FakeSwagLobby, mockUserSession.Object);

            // Act
            var result = sut.Lobby(_lobbyViewModel) as ViewResult;

            // Assert
            Assert.IsInstanceOf<LobbyViewModel>(result.Model);
        }

        [Test]
        public void ForladLobby_UserOKreturnsLobbyView()
        {

            // Arrange
            var mockUserSession = new Mock<IUserSession>();
            mockUserSession.Setup(x => x.User).Returns(_savedUser);

            var sut = new LobbyController(FakeSwagCommunication, FakeSwagLobby, mockUserSession.Object);

            // Act
            var result = (RedirectToActionResult) sut.ForladLobby(_lobbyViewModel);

            // Assert
            Assert.AreEqual("TilslutLobby", result.ActionName);
        }

    }
}
