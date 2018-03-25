using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace GUI_Index.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult LogInd()
        {
            return View();
        }

        public IActionResult OpretKonto()
        {
            return View();
        }

        public IActionResult PostLogInd()
        {
            return View();
        }

    }
}
