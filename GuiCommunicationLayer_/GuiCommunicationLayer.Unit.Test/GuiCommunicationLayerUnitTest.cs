using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace GuiCommunicationLayer.Unit.Test
{

	public class GuiCommunicationLayerUnitTest
	{
		private SwagCommunication _uut;
		private User testUser;
		[SetUp]
		public void Init()
		{
			_uut = new SwagCommunication("https://f7f2fec5-c272-427f-af39-98ad7b43220a.mock.pstmn.io");
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
			var url = SwagCommunication.CreateUserAsync(testUser).Result;
			
			//Assert
			Assert.That(url, Is.EqualTo(""));
		}

		[Test]
		public void GetUserAsync_Correct()
		{
			//Arrange

			//Act
			var url = SwagCommunication.GetUserAsync(testUser.Username, testUser.Password).Result;

			//Assert
			Assert.That(url, Is.EqualTo(testUser));
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
