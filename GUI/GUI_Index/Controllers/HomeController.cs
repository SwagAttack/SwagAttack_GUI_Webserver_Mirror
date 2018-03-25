using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GUI_Index.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GUI_Index.Controllers
{
    public class HomeController : Controller
    {
        public  static List<User> UserList {get; set; }

        public IActionResult LogInd()
        {
            if (UserList == null)
                UserList = new List<User>();

            return View();
        }

        [HttpPost]
        public IActionResult LogInd(User user)
        {
            foreach (User item in UserList)
            {
                if (item.Username == user.Username && item.Password == user.Password)
                {
                    return View("PostLogInd");
                }

            }
            return View("LogInd");
        }

        public IActionResult OpretKonto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OpretKonto(User user)
        {
            try
            {
                
                UserList.Add(user);
                return RedirectToAction("LogInd");
            }
            catch 
            {
                return RedirectToAction("OpretKonto");
            }
            


            return RedirectToAction("LogInd");
        }

        public IActionResult PostLogInd()
        {
            return View();
        }

    }
}
