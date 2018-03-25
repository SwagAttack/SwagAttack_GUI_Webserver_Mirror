using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GUI_Index.Models;
using Microsoft.AspNetCore.Mvc;


namespace GUI_Index.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult LogInd()
        {
            return View();
        }

        public IActionResult LogIndTryk()
        {
            return View("PostLogInd");
        }

        public IActionResult OpretKonto()
        {
            return View();
        }

        public IActionResult NyKonto(User user)
        {
            return View("LogInd",User);
        }

        public IActionResult PostLogInd()
        {
            return View();
        }

    }
}
