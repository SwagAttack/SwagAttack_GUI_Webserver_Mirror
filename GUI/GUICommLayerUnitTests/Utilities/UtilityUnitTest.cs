using Domain.Interfaces;
using GUICommLayer.Proxies.Utilities;
using NSubstitute;
using NUnit.Framework;

namespace GUICommLayerUnitTests.Proxies
{
    [TestFixture]
    public class UtilityUnitTest
    {
        [Test]
        public void ComposeJson_ObjectIsIUser_ReturnsCorrectJsonObject()
        {
            //arrange
            var testUser = Substitute.For<IUser>();
            testUser.Username = "MyTestUsername";
            testUser.Password = "MyTestPassword";

            //act
            var result = Utility.ComposeJson<IUser>(testUser.Username, testUser.Password, testUser);

            //assert
            Assert.That(result.SelectToken("auth").SelectToken("Username").ToString(), Is.EqualTo(testUser.Username));
            Assert.That(result.SelectToken("auth").SelectToken("Password").ToString(), Is.EqualTo(testUser.Password));
            Assert.That(result.SelectToken("val").SelectToken("Username").ToString(), Is.EqualTo(testUser.Username));
            Assert.That(result.SelectToken("val").SelectToken("Password").ToString(), Is.EqualTo(testUser.Password));

        }

        [Test]
        public void ComposeJson_ObjectIsNull_ReturnsCorrectJsonObject()
        {
            //arrange
            var testUser = Substitute.For<IUser>();
            testUser.Username = "MyTestUsername";
            testUser.Password = "MyTestPassword";

            //act
            var result = Utility.ComposeJson<IUser>(testUser.Username, testUser.Password, null);

            //assert
            Assert.That(result.SelectToken("auth").SelectToken("Username").ToString(), Is.EqualTo(testUser.Username));
            Assert.That(result.SelectToken("auth").SelectToken("Password").ToString(), Is.EqualTo(testUser.Password));
            Assert.That(result.SelectToken("val").SelectToken("Username").ToString(), Is.EqualTo(testUser.Username));
            Assert.That(result.SelectToken("val").SelectToken("Password").ToString(), Is.EqualTo(testUser.Password));

        }
    }
}
