using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GUICommLayer;
using GUI_Index;
using GUI_Index.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Interfaces;
using Models.User;

namespace GUI_Index.Controllers
{
    public class HomeController : Controller, IHomeController
    {
        private SwagCommunication _swag;
        public HomeController(ISwagCommunication somuchswag)
        {
            _swag = somuchswag as SwagCommunication;
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
                var sendUser = _swag.GetUserAsync(user.Username,user.Password);
                
                sendUser.Wait();
                var tmp = sendUser.Result;
                if (sendUser.Result != null)
                {
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
        public IActionResult  OpretKonto(User user)
        {
            try
            {
                var sendUser = _swag.CreateUserAsync(user);
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

        public IActionResult Lobby()
        {
            return View("Lobby");
        }

        public IActionResult TilslutLobby()
        {
            return View("TilslutLobby");
        }

    }
}
