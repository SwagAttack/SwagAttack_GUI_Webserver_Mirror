using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1;
using GUI_Index.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GUI_Index.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult LogInd()
        {
            return View("LogInd");
        }

        [HttpPost]
        public IActionResult LogInd(User user)
        {
            try
            {
                SwagClient client = new SwagClient("127.0.0.1");
                JSONConverter nyBruger = new JSONConverter();
                string res = nyBruger.LogInUser(user);
                var sendUser = client.SendString(res);
                if (sendUser == "ok")
                {
                    return RedirectToAction("PostLogInd", user);
                }


                
            }
            catch (Exception e)
            {
               
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
            //bool flag = true;
            try
            {
	            SwagClient client = new SwagClient("127.0.0.1");
	            JSONConverter nyBruger = new JSONConverter();
	            string res = nyBruger.NewUser(user);

                if (client.SendString(res) == "ok")
                {
                    //Do something if user is allowed to be created.
                    return RedirectToAction("LogInd");
                }
                return RedirectToAction("OpretKonto");


            }

            catch 
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
