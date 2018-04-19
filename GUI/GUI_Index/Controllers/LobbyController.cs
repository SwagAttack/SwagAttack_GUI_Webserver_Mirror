using System;
using System.Collections.Generic;
using System.Linq;
using GUICommLayer;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GUI_Index.Controllers
{
    public class LobbyController : Controller
    {
        private static List<ILobby> lobbyList = new List<ILobby>(); // skal bindes sammen med hub når den kommer
        private SwagCommunication _swag;

        public LobbyController(ISwagCommunication somuchswag)
        {
            _swag = somuchswag as SwagCommunication;
        }

        public IActionResult CreateLobby(IUser adminUser)
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

        public IActionResult TilslutLobby(string username,string password)
        {

            //for test
            IUser x = new User();
            x.Email = "poul@poul.dk";
            x.GivenName = "poul";
            x.LastName = "poul";
            x.Password = "1234asdsadsadsa";
            x.Username = "adminPoul";

            ILobby y = new Lobby(x);
            
            y.Id = "lobbien";
            lobbyList.Add(y);
            return View(lobbyList);
        }

        public IActionResult Lobby(string password, string username,string lobbyId)
        {
            //find the user lol
            IUser user = _swag.GetUserAsync(username, password).Result;
            //add user to the lobby
            lobbyList.Find(x => x.Id == lobbyId).AddUser(user);

            //go to the lobby
            return View(lobbyList.Find(x => x.Id == lobbyId));
        }

    }
}