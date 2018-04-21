using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using GUICommLayer.Proxies.Utilities;
using NUnit.Framework;

namespace GUICommLayerUnitTests.Proxies
{
    [TestFixture]
    public class ClientUnitTest
    {
        [Test]
        public void GetInstance_ReturnsCorrectType()
        {
            //arrange
            var uut = new Client();

            //act
            var result = uut.GetInstance();
            
            //assert
            Assert.That(result.GetType(), Is.EqualTo(typeof(HttpClient)));
        }
    }
}
