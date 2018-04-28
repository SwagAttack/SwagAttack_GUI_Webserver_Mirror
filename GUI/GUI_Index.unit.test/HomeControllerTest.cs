﻿using System;
using GUICommLayer;
using GUI_Index.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Domain.Models;
using GUICommLayer.Interfaces;

namespace WebserverUnitTests
{
    [TestFixture]
    public class HomeControllerTest
    {
        private IUserProxy FakeSwagCommunication = Substitute.For<IUserProxy>();

        [Test]
        public void HomeControllerLogInd_ViewNameCorrect()
        {
            var uut = new HomeController(FakeSwagCommunication);

            var result = uut.LogInd() as ViewResult;
            
            Assert.AreEqual("LogInd", result.ViewName);
        }

        [Test]
        public void HomeControllerLogIndWithIncorrectUser_ViewNameCorrect()
        {
            var uut = new HomeController(FakeSwagCommunication);
            var wrongUser = new User();
            var result = uut.LogInd(wrongUser) as ViewResult;
            
            Assert.AreEqual("LogInd", result.ViewName);
        }

        //[Test]
        //public void HomeControllerOpretKonto_ViewNameCorrect()
        //{
        //    var uut = new KontoController(FakeSwagCommunication);
        //    var result = uut.OpretKonto() as ViewResult;


        //    Assert.AreEqual("OpretKonto", result.ViewName);
        //}

        //[Test]
        //public void HomeControllerOpretIncorrectUser_ExceptionThrownFromDomainLayer()
        //{
        //    var uut = new KontoController(FakeSwagCommunication);
            
        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        var wrongUser = new User() { Username = "†gagge1233121" };
        //        var result = uut.OpretKonto(wrongUser) as RedirectToActionResult;
        //    });
        //}

        [Test]
        public void HomeControllerPostLogInd_ViewNameCorrect()
        {
            var uut = new HomeController(FakeSwagCommunication);
            var result = uut.PostLogInd(new User(){Username="PatrickBjerregaard"}) as ViewResult;


            Assert.AreEqual("PostLogInd", result.ViewName);
        }
    }
}
