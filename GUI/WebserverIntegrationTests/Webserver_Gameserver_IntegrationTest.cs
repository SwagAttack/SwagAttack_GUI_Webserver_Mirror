using System;
using Domain.Interfaces;
using GUICommLayer;
using GUI_Index;
using GUI_Index.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Domain.Models;
using GUICommLayer.Proxies;
using GUICommLayer.Proxies.Utilities;
using GUI_Index.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace WebserverIntegrationTests
{
    [TestFixture]
    public class WebserverGameserverIntegrationTest
    {
        [Test]
        public void HomeControllerLogInd_ViewNameCorrect()
        {
            var uut = new HomeController(new UserProxy(new Client()));
            var result = uut.LogInd() as ViewResult;
            
            Assert.AreEqual("LogInd", result.ViewName);
        }

        [Test]
        public void HomeControllerLogIndWithIncorrectUser_ViewNameCorrect()
        {
            var uut = new HomeController(new UserProxy(new Client()));
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

        //[Test]
        //public void HomeControllerPostLogInd_ViewNameCorrect()
        //{
        //    var uut = new HomeController(new UserProxy(new Client()))            /*var tmp = new User();
        //    tmp.Username = "lellefader";
        //    tmp.Password = "123456789";
        //    tmp.Email = "gobbenobber@gmail.com";
        //    tmp.GivenName = "Patrick";
        //    tmp.LastName = "Bjerregaard";
        //    var httpContextSession = uut.HttpContext.Session;*/
        //    var result = uut.PostLogInd() as ViewResult;

        //    Assert.AreEqual("PostLogInd", result.ViewName);
        //}
    }
}
