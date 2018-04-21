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
        /// <summary>
        /// LogIndView
        /// </summary>
        /// <returns></returns>
        IActionResult LogInd();

        /// <summary>
        /// Log Ind button pushed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IActionResult LogInd(User user);
        
        ///// <summary>
        ///// OpretKontoView
        ///// </summary>
        ///// <returns></returns>
        //IActionResult OpretKonto();

        /// <summary>
        /// Opret konto pushed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
       // IActionResult OpretKonto(User user);

        /// <summary>
        /// PostLogIndView
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IActionResult PostLogInd(User user);
    }
}
