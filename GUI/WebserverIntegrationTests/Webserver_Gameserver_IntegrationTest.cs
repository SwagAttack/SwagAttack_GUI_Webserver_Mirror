using System;
using GUICommLayer;
using GUI_Index;
using GUI_Index.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Domain.Models;

namespace WebserverIntegrationTests
{
    [TestFixture]
    public class WebserverGameserverIntegrationTest
    {
        private string url = "https://swagattkapi.azurewebsites.net/";
        [Test]
        public void HomeControllerLogInd_ViewNameCorrect()
        {
            var uut = new HomeController(SwagCommunication.GetInstance(url));
            var result = uut.LogInd() as ViewResult;
            
            Assert.AreEqual("LogInd", result.ViewName);
        }

        [Test]
        public void HomeControllerLogIndWithIncorrectUser_ViewNameCorrect()
        {
            var uut = new HomeController(SwagCommunication.GetInstance(url));
            var wrongUser = new User();
            var result = uut.LogInd(wrongUser) as ViewResult;
            
            Assert.AreEqual("LogInd", result.ViewName);
        }

        [Test]
        public void HomeControllerOpretKonto_ViewNameCorrect()
        {
            var uut = new HomeController(SwagCommunication.GetInstance(url));
            var result = uut.OpretKonto() as ViewResult;


            Assert.AreEqual("OpretKonto", result.ViewName);
        }

        [Test]
        public void HomeControllerOpretIncorrectUser_ViewNameCorrect()
        {
            var uut = new HomeController(SwagCommunication.GetInstance(url));

            Assert.Throws<AggregateException>(() =>
            {
                var wrongUser = new User() { Username = "PatrickBjerregaard" };
                var result = uut.OpretKonto(wrongUser) as RedirectToActionResult;
            });
        }

        [Test]
        public void HomeControllerPostLogInd_ViewNameCorrect()
        {
            var uut = new HomeController(SwagCommunication.GetInstance(url));
            var result = uut.PostLogInd(new User(){Username="PatrickBjerregaard"}) as ViewResult;


            Assert.AreEqual("PostLogInd", result.ViewName);
        }
    }
}
