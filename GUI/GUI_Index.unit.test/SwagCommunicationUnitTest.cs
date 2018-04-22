using System;
using System.Collections.Generic;
using System.Text;
using GUICommLayer;
using GUI_Index.Interfaces;
using NUnit.Framework;
using Domain.Interfaces;
using Domain.Models;
namespace GUI_Index.unit.test
{
    class SwagCommunicationUnitTest
    {
            //link to MockServer created via Postman
            private static string url = "https://f7f2fec5-c272-427f-af39-98ad7b43220a.mock.pstmn.io/";

           // private SwagCommunication _uut = new SwagCommunication(url);

            private User testUser;

            [SetUp]
            public void Init()
            {

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
                var uut = SwagCommunication.GetInstance(url).CreateUserAsync(testUser).Result;

                //Assert
                Assert.That(uut.ToString(), Is.EqualTo(url + "api/user/" + testUser.Username + "/" + testUser.Password));
            }

            [Test]
            public void CreateUserAsync_Correct_ReturnsExeption()
            {
                //Arrange

                //Act
                var uut = SwagCommunication.GetInstance(url).CreateUserAsync(testUser).Result;

                //Assert
                Assert.That(uut.ToString(), Is.EqualTo(url + "api/user/" + testUser.Username + "/" + testUser.Password));
            }

            [Test]
            public void GetUserAsync_Correct_ReturnsTestUser()
            {
                //Arrange

                //Act
                var uut = SwagCommunication.GetInstance(url).GetUserAsync(testUser.Username, testUser.Password).Result;
                
                //Asserts
                Assert.That(uut.ToString(), Is.EqualTo(testUser.ToString()));
            }

            [Test]
            public void GetUserAsync_DoesntExist_ReturnNullObject()
            {
                //Arrange

                //Act
                var uut = SwagCommunication.GetInstance(url).GetUserAsync(testUser.Username, "WRONG").Result;


                //Asserts
                Assert.That(uut, Is.EqualTo(null));
            }

            
        
    }
}
