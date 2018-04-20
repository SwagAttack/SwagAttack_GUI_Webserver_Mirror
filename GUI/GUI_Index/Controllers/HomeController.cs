using System;
using Domain.Interfaces;
using Domain.Models;
using GUICommLayer;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies;
using GUI_Index.Interfaces;
using GUI_Index.Session;
using Microsoft.AspNetCore.Mvc;

namespace GUI_Index.Controllers
{
    public class HomeController : Controller, IHomeController
    {
        private UserProxy _proxy;

        public HomeController(IUserProxy userProxy)
        {
            _proxy = userProxy as UserProxy;
        }

        public IActionResult LogInd()
        {
            return View("LogInd");
        }

        [HttpPost]
        public IActionResult LogInd(User user)
        {
            try
            {
                var sendUser = _proxy.RequestInstanceAsync(user.Username, user.Password);

                sendUser.Wait();
                var tmp = sendUser.Result;
                if (sendUser.Result != null)
                {
                    //set user to session
                    SessionExtension.SetObjectAsJson(HttpContext.Session, "user", tmp);

                    return RedirectToAction("PostLogInd", tmp);
                }

            }
            catch (Exception e)
            {
                var tmp = e.GetBaseException().Message;
            }

            return View("LogInd");
        }

        public IActionResult OpretKonto()
        {
            return View("OpretKonto");
        }

        [HttpPost]
        public IActionResult OpretKonto(User user)
        {
            try
            {
                var sendUser = _proxy.CreateInstanceAsync(user);
                sendUser.Wait();
                if (sendUser.Result != null)
                {
                    return RedirectToAction("LogInd");
                }

                return RedirectToAction("OpretKonto");

            }

            catch (ArgumentException e)
            {
                return RedirectToAction("OpretKonto");
            }
        }

        public IActionResult PostLogInd(User user)
        {
            return View("PostLogInd");
        }

       
    }
}

