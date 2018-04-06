using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GUI_Index;
using GUI_Index.Interfaces;
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
                //SwagClient client = new SwagClient("127.0.0.1");
                //JSONConverter nyBruger = new JSONConverter();
                //string res = nyBruger.LogInUser(user);
                

                var sendUser = SwagCommunication.GetUserAsync(user.Username,user.Password);


                //var sendUser = client.SendString(res);

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
            //bool flag = true;
            try
            {
                //SwagClient client = new SwagClient("127.0.0.1");
                //JSONConverter nyBruger = new JSONConverter();
                //string res = nyBruger.NewUser(user);

                


                var sendUser = SwagCommunication.CreateUserAsync(user).Result;
               
                
                if (sendUser != null)
                {
                    //Do something if user is allowed to be created.
                    return RedirectToAction("LogInd");
                }

                // if (client.SendString(res) == "ok")
                // {
                //Do something if user is allowed to be created.
                //     return RedirectToAction("LogInd");
                // }
                return RedirectToAction("OpretKonto");
            }



            catch(Exception e)
            {
                
                var stuff = e.GetBaseException().Message;
                Console.WriteLine(e.GetBaseException().Message);
                return RedirectToAction("OpretKonto");
            }

        }

        public IActionResult PostLogInd(User user)
        {
            return View("PostLogInd");
        }

    }
}
