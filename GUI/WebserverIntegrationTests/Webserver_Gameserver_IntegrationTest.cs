using GUI_Index.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Domain.Models;
using GUICommLayer.Proxies;
using GUICommLayer.Proxies.Utilities;

namespace WebserverIntegrationTests
{
    [TestFixture]
    public class WebserverGameserverIntegrationTest
    {
        [Test]
        public void HomeControllerLogInd_ViewNameCorrect()
        {
            var uut = new HomeController(new UserProxy(new HttpRequestFactory(new Client(), "https://swagattackapi.azurewebsites.net/")));
            var result = uut.LogInd() as ViewResult;
            
            Assert.AreEqual("LogInd", result.ViewName);
        }

        [Test]
        public void HomeControllerLogIndWithIncorrectUser_ViewNameCorrect()
        {
            var uut = new HomeController(new UserProxy(new HttpRequestFactory(new Client(), "https://swagattackapi.azurewebsites.net/")));
            var wrongUser = new User();
            var result = uut.LogInd(wrongUser) as ViewResult;
            
            Assert.AreEqual("LogInd", result.ViewName);
        }

        //[Test]
        //public void KontoControllerOpretKonto_ViewNameCorrect()
        //{
        //    var uut = new KontoController(new UserProxy(new Client()));
        //    var result = uut.OpretKonto() as ViewResult;


        //    Assert.AreEqual("OpretKonto", result.ViewName);
        //}

        //[Test]
        //public void KontoControllerOpretIncorrectUser_ViewNameCorrect()
        //{
        //    var uut = new KontoController(new UserProxy(new Client()));

        //    Assert.Throws<AggregateException>(() =>
        //    {
        //        var wrongUser = new User() { Username = "PatrickBjerregaard" };
        //        var result = uut.OpretKonto(wrongUser) as RedirectToActionResult;
        //    });
        //}

        [Test]
        public void HomeControllerPostLogInd_ViewNameCorrect()
        {
            var uut = new HomeController(new UserProxy(new HttpRequestFactory(new Client(), "https://swagattackapi.azurewebsites.net/")));
            var result = uut.PostLogInd() as ViewResult;


            Assert.AreEqual("PostLogInd", result.ViewName);
        }
    }
}
