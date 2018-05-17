using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies;
using NUnit.Framework;
using Moq;
using Newtonsoft.Json;
using NSubstitute;

namespace UnitTests
{
	[TestFixture]
	class GameProxyUnitTest
    {
		private Mock<IHttpRequestFactory> _fakeHttpRequestFactory = new Mock<IHttpRequestFactory>();
	    private Mock<IHttpRequestBuilder> _fakeHttpRequestBuilder = new Mock<IHttpRequestBuilder>();

		private IGameProxy _uut;

	    private string _username;
	    private string _password;
	    private string _gameId;
	    

		[SetUp]
	    public void Init()
		{
			_uut = new GameProxy(_fakeHttpRequestFactory.Object);
			_username = "Username";
			_password = "Pasword";
			_gameId = "GameIdString";
			
		}

	    [Test]
	    public void CreateInstanceAsyc_called_correct()
	    {
			//Arrange
			List<string> playerlist = new List<string>() { "player1", "player2" };

		    var mockResponseMessage = new Mock<HttpResponseMessage>();
		    mockResponseMessage.Object.StatusCode = HttpStatusCode.OK;
			mockResponseMessage.Object.Content = 
				new StringContent(JsonConvert.SerializeObject(
					new Game("testGameId", playerlist)));


			//setup sendAsync to reply ok
			_fakeHttpRequestBuilder.Setup(
				reply => reply.SendAsync())
					.Returns(Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK)));
		

			_fakeHttpRequestFactory.Setup(
				    reply => reply.Post(It.Is<string>(s => s.Equals("/api/Game/Create")),null)
					    .AddAuthentication(
						    It.Is<string>(s => s.Equals(_username)),
						    It.Is<string>(s => s.Equals(_password)))
					    .AddUriQuery(
						    It.Is<string>(s => s.Equals("GameId")),
						    It.Is<string>(s => s.Equals(_gameId)))
					    .AddContent(It.Is<List<string>>(s => s.Equals(playerlist))))
			    .Returns(_fakeHttpRequestBuilder.Object);

			


		    //Act
		    //Assert
	    }
    }
}
