using GUICommLayer;
using Models.Interfaces;
using Models.User;
using NUnit.Framework;

namespace WebserverUnitTests
{
    class SwagCommunicationUnitTest
    {
            //link to MockServer created via Postman
            private static string url = "https://f7f2fec5-c272-427f-af39-98ad7b43220a.mock.pstmn.io/";

           // private SwagCommunication _uut = new SwagCommunication(url);

            private User _testUser;
            private User _testUser2;

            private IUser _wrongTestUser;

            [SetUp]
            public void Init()
            {

                _testUser = new User
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
                var uut = SwagCommunication.GetInstance(url).CreateUserAsync(_testUser).Result;

                //Assert
                Assert.That(uut.ToString(), Is.EqualTo(url + "api/user/" + _testUser.Username + "/" + _testUser.Password));
            }

            [Test]
            public void CreateUserAsync_Correct_ReturnsExeption()
            {
                //Arrange

                //Act
                var uut = SwagCommunication.GetInstance(url).CreateUserAsync(_testUser).Result;

                //Assert
                Assert.That(uut.ToString(), Is.EqualTo(url + "api/user/" + _testUser.Username + "/" + _testUser.Password));
            }

            [Test]
            public void GetUserAsync_Correct_ReturnsTestUser()
            {
                //Arrange

                //Act
                var uut = SwagCommunication.GetInstance(url).GetUserAsync(_testUser.Username, _testUser.Password).Result;
                
                //Asserts
                Assert.That(uut.ToString(), Is.EqualTo(_testUser.ToString()));
            }

            [Test]
            public void GetUserAsync_DoesntExist_ReturnNullObject()
            {
                //Arrange

                //Act
                var uut = SwagCommunication.GetInstance(url).GetUserAsync(_testUser.Username, "WRONG").Result;


                //Asserts
                Assert.That(uut, Is.EqualTo(null));
            }

            
        
    }
}
