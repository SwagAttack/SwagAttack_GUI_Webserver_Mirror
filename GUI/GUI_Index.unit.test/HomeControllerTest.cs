using GUI_Index.Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.User;
using NUnit.Framework;

namespace GUI_Index.unit.test
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void HomeControllerLogInd_ViewNameCorrect()
        {
            var uut = new HomeController();

            var result = uut.LogInd() as ViewResult;
            
            Assert.AreEqual("LogInd", result.ViewName);
        }

        [Test]
        public void HomeControllerLogIndWithIncorrectUser_ViewNameCorrect()
        {
            var uut = new HomeController();
            var wrongUser = new User();
            var result = uut.LogInd(wrongUser) as ViewResult;
            
            Assert.AreEqual("LogInd", result.ViewName);
        }

        [Test]
        public void HomeControllerOpretKonto_ViewNameCorrect()
        {
            var uut = new HomeController();
            var result = uut.OpretKonto() as ViewResult;

            Assert.AreEqual("OpretKonto", result.ViewName);
        }

        [Test]
        public void HomeControllerOpretIncorrectUser_ViewNameCorrect()
        {
            var uut = new HomeController();
            var wrongUser = new User(){Username="PatrickBjerregaard"};
            var result =uut.OpretKonto(wrongUser) as RedirectToActionResult;

            Assert.AreEqual("OpretKonto", result.ActionName);
        }

        [Test]
        public void HomeControllerPostLogInd_ViewNameCorrect()
        {
            var uut = new HomeController();
            var result = uut.PostLogInd(new User(){Username="PatrickBjerregaard"}) as ViewResult;

            Assert.AreEqual("PostLogInd", result.ViewName);
        }
    }
}
