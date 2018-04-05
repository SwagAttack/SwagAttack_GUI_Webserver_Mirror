using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace GuiCommunicationLayer.Unit.Test
{

	public class GuiCommunicationLayerUnitTest
	{

        //link to MockServer created via Postman
	    private string url = "https://f7f2fec5-c272-427f-af39-98ad7b43220a.mock.pstmn.io/";

        private SwagCommunication _uut;
		private User testUser;
		[SetUp]
		public void Init()
		{
			_uut = new SwagCommunication(url);
			testUser = new User();

			testUser.Email = "test@testsen.dk";
			testUser.GivenName = "Hr";
			testUser.LastName = "testsen";
			testUser.Password = "12345678o";
			testUser.Username = "TheTestMan";

		}

		[Test]
		public void CreateUserAsync_Correct()
		{
			//Arrange

			//Act
			var uut = SwagCommunication.CreateUserAsync(testUser).Result;
			
			//Assert
			Assert.That(uut.ToString(), Is.EqualTo(url + "api/user/" + testUser.Username + "/"+ testUser.Password));
		}

		[Test]
		public void GetUserAsync_Correct()
		{
			//Arrange

			//Act
			var url = SwagCommunication.GetUserAsync(testUser.Username, testUser.Password);
		    url.Wait();


			//Asserts
			Assert.That(url.Result.Email, Is.EqualTo(testUser.Email));
		    Assert.That(url.Result.Username, Is.EqualTo(testUser.Username));
		    Assert.That(url.Result.GivenName, Is.EqualTo(testUser.GivenName));
		    Assert.That(url.Result.LastName, Is.EqualTo(testUser.LastName));
		    Assert.That(url.Result.Password, Is.EqualTo(testUser.Password));
        }

		[Test]
		public void UpdateUserAsync_Correct()
		{
			//Arrange
			User testUser2 = testUser;
			testUser2.Password = "12345678i";
			
			//Act
			var reply = SwagCommunication.UpdateUserAsync(testUser, testUser2.Password).Result;
			
			//Assert
			Assert.That(reply, Is.EqualTo(testUser2));
		}

	}
}
