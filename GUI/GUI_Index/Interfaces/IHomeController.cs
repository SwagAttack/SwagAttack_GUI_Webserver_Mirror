using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

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
