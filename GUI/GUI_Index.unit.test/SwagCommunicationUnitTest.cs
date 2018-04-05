using System;
using System.Collections.Generic;
using System.Text;
using GUI_Index.Interfaces;
using GUI_Index.Models;
using NUnit.Framework;

namespace GUI_Index.unit.test
{
    class SwagCommunicationUnitTest
    {
        

            //link to MockServer created via Postman
            private static string url = "https://f7f2fec5-c272-427f-af39-98ad7b43220a.mock.pstmn.io/";

            private SwagCommunication _uut = new SwagCommunication(url);

            private User testUser;

            private IUser wrongTestUser;

            [SetUp]
            public void Init()
            {
                
                testUser = new User
                {
                    id = "test@testsen.dk",
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
                Assert.That(uut.ToString(), Is.EqualTo(url + "api/user/" + testUser.Username + "/" + testUser.Password));
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
                Assert.That(url.ToString(), Is.EqualTo(testUser.ToString()));
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

            
        
    }
}
