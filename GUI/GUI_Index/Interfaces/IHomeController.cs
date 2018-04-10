using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Interfaces;
using Models.User;

namespace GUI_Index.Interfaces
{
    interface IHomeController
    {
        IActionResult LogInd();
        IActionResult LogInd(User user);
        IActionResult OpretKonto();
        IActionResult OpretKonto(User user);
        IActionResult PostLogInd(User user);
    }
}
