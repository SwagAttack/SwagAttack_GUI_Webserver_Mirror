
using System;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies;
using GUI_Index.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GUI_Index.Controllers
{
    public class KontoController : Controller
    {
	    private UserProxy _proxy;

	    public KontoController(IUserProxy userProxy)
	    {
		    _proxy = userProxy as UserProxy;
	    }

		[HttpGet]
        public IActionResult OpretKonto()
        {
	        return View("OpretKonto");
        }

	    [HttpPost]
	    public IActionResult OpretKonto(RegistorViewModel vm)
	    {
			//Check for valid inpit
		    if (ModelState.IsValid)
			{ 
				var user = new User
				{
					Username = vm.Username,
					Email = vm.Email,
					GivenName = vm.GivenName,
					LastName = vm.LastName,
					Password = vm.Password
				};

				//Try to create user
				try
				{
					var sendUser = _proxy.CreateInstanceAsync(user);
					sendUser.Wait();
					if (sendUser.Result != null)
					{
						return RedirectToAction("LogInd","Home");
					}

					//return RedirectToAction("OpretKonto");
					return RedirectToAction("OpretKonto", "Konto");

				}

				catch (ArgumentException)
				{
					return RedirectToAction("OpretKonto", "Konto");
				}
			}
			return View(vm);
		}
	}
}