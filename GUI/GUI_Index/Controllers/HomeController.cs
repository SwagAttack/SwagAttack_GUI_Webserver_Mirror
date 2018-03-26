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
            //if (UserList.Users == null)
            //    UserList.Users = new List<User>();

            return View();
        }

        [HttpPost]
        public IActionResult LogInd(User user)
        {
            SwagClient client = new SwagClient("127.0.0.1");
            JSONConverter nyBruger = new JSONConverter();
            string res = nyBruger.logInUser(user);
            var sendUser = client.SendString(res);

            if (sendUser == "ok")
            {
                return View("PostLogInd", user);
            }

            //foreach (User item in UserList.Users)
            //{
            //    if (item.Username == user.Username && item.Password == user.Password)
            //    {
            //        return View("PostLogInd",item);
            //    }

            //}

            return View("LogInd");
        }

        public IActionResult OpretKonto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OpretKonto(User user)
        {
            //bool flag = true;
            try
            {
	            SwagClient client = new SwagClient("127.0.0.1");
	            JSONConverter nyBruger = new JSONConverter();
	            string res = nyBruger.newUser(user);

                if (client.SendString(res) == "ok")
                {
                    //Do something if user is allowed to be created.
                    return RedirectToAction("LogInd");
                }
                return RedirectToAction("OpretKonto");
               
                

                
                //ugly fix for no data storage
                //			if (UserList.Users.Count != 0)
                //            {
                //                foreach (User item in UserList.Users)
                //                {
                //                    if (item.Username == user.Username && item.Password == user.Password)
                //                    {
                //                        flag = false;
                //                    }
                //                }
                //            }

                //if (flag)
                //{
                //    UserList.Users.Add(user);
                //    return RedirectToAction("LogInd");
                //}

            }

            catch 
            {
                return RedirectToAction("OpretKonto");
            }
            
        }

        public IActionResult PostLogInd(User user)
        {
            return View();
        }

        //public IActionResult UserListView()
        //{
        //    var model = UserList.Users;
        //    return View(model);
        //}

    }
}
