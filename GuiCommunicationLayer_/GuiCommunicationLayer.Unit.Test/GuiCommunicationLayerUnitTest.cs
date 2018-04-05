using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace GuiCommunicationLayer.Unit.Test
{

	public class GuiCommunicationLayerUnitTest
	{

        //link to MockServer created via Postman
	    private static string url = "https://f7f2fec5-c272-427f-af39-98ad7b43220a.mock.pstmn.io/";

        private SwagCommunication _uut = new SwagCommunication(url);

		private User testUser;

        private User wrongTestUser;

		[SetUp]
		public void Init()
		{;
		    testUser = new User
		    {
		        Email = "test@testsen.dk",
		        GivenName = "Hr",
		        LastName = "testsen",
		        Password = "12345678o",
		        Username = "TheTestMan"
		    };
		}

		[Test]
		public void CreateUserAsync_Correct_ReturnsTestUserURL()
		{
			//Arrange

			//Act
			var uut = SwagCommunication.CreateUserAsync(testUser).Result;
			
			//Assert
			Assert.That(uut.ToString(), Is.EqualTo(url + "api/user/" + testUser.Username + "/"+ testUser.Password));
		}

	    [Test]
	    public void CreateUserAsync_Correct_ReturnsExeption()
	    {
	        //Arrange

	        //Act
	        var uut = SwagCommunication.CreateUserAsync(testUser).Result;

	        //Assert
	        Assert.That(uut.ToString(), Is.EqualTo(url + "api/user/" + testUser.Username + "/" + testUser.Password));
	    }

        [Test]
		public void GetUserAsync_Correct_ReturnsTestUser()
		{
			//Arrange

			//Act
			var url = SwagCommunication.GetUserAsync(testUser.Username, testUser.Password).Result;


			//Asserts
			Assert.That(url.Email, Is.EqualTo(testUser.Email));
		    Assert.That(url.Username, Is.EqualTo(testUser.Username));
		    Assert.That(url.GivenName, Is.EqualTo(testUser.GivenName));
		    Assert.That(url.LastName, Is.EqualTo(testUser.LastName));
		    Assert.That(url.Password, Is.EqualTo(testUser.Password));
        }

	    [Test]
	    public void GetUserAsync_DoesntExist_ReturnNullObject()
	    {
	        //Arrange

	        //Act
	        var url = SwagCommunication.GetUserAsync(testUser.Username, "WRONG").Result;


	        //Asserts
	        Assert.That(url, Is.EqualTo(null));
	    }

        [Test]
		public void UpdateUserAsync_Correct()
		{
			//Arrange
			testUser.Password = "12345678i";
			
			//Act
			var reply = SwagCommunication.UpdateUserAsync(testUser, testUser.Password).Result;
			
			//Assert
			Assert.That(reply, Is.EqualTo(testUser));
		}

	}
}
