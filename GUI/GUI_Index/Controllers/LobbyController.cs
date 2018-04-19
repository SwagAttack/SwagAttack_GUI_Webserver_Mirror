using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GUI_Index.Controllers
{
    public class LobbyController : Controller
    {
        private List<ILobby> lobbyList = new List<ILobby>();
        public IActionResult CreateLobby()
        {
            return View();
        }

        //save the lobby to controller 
        [HttpPost]
        public async Task<IActionResult> CreateLobby([Bind("LobbyID,AdminName")] ILobby ConcreteLobby)
        { 
            lobbyList.Add(ConcreteLobby);
            return RedirectToAction(nameof(Lobby));
        }

        public IActionResult TilslutLobby()
        {
            
            return View(lobbyList);
        }

        public IActionResult Lobby(string username,string lobbyId)
        {
            return View();
        }

    }
}