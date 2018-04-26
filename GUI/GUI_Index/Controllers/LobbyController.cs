using Domain.Interfaces;
using Domain.Models;
using GUI_Index.Session;
using GUI_Index.ViewModels;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GUI_Index.Controllers
{
    public class LobbyController : Controller
    {
        private static List<ILobby> _lobbyList = new List<ILobby>();
        private UserProxy _proxy;

        public LobbyController(IUserProxy userProxy)
        {
            _proxy = userProxy as UserProxy;
        }

        public IActionResult OpretLobby()
        {
            return View("OpretLobby");
        }

        [HttpPost]
        public IActionResult OpretLobby(LobbyViewModel lobby)
        {

            try
            {
                //find brugeren der har lavet lobby
                var currentUser = HttpContext.Session.GetObjectFromJson<User>("user");
                //save as a lobby
                ILobby nyLobby = new Lobby(currentUser)
                {
                    Id = lobby.Id
                };
                SessionExtension.SetObjectAsJson(HttpContext.Session, lobby.Id, nyLobby);
                //add to the list
                _lobbyList.Add(nyLobby);
                return RedirectToAction("Lobby","Lobby",lobby);

            }
            catch (ArgumentException)
            {
                return RedirectToAction("OpretLobby");
            }
            
        }

        public IActionResult TilslutLobby()
        {
            var currentUser = HttpContext.Session.GetObjectFromJson<User>("user");

            return View(_lobbyList);
        }

        [HttpPost]
        public IActionResult TilslutLobby(LobbyViewModel model)
        {
            return RedirectToAction("Lobby", model);
        }

        public IActionResult Lobby(LobbyViewModel lobbyId)
        {
            var currentUser = HttpContext.Session.GetObjectFromJson<User>("user");
            //add user to the lobby if it isent on list already.
            if (!_lobbyList.Find(x => x.Id == lobbyId.Id).Usernames.Any(x => x.Contains(currentUser.Username)))
            {
                _lobbyList.Find(x => x.Id == lobbyId.Id).AddUser(currentUser);
            }

            ////go to the lobby
            return View(_lobbyList.Find(x => x.Id == lobbyId.Id));
        }

        public IActionResult ForladLobby(string lobbyId)
        {
            var currentUser = HttpContext.Session.GetObjectFromJson<User>("user");
            _lobbyList.Find(x => x.Id == lobbyId).RemoveUser(currentUser);

            //need method to check if its admin, and update a new admin.
            if (_lobbyList.Find(x => x.Id == lobbyId).AdminUserName == currentUser.Username)
            {
                _lobbyList.Find(x=> x.Id == lobbyId).UpdateAdmin();
            }

            //remove lobby if empty
            if (_lobbyList.Find(x => x.Id == lobbyId).Usernames.Any())
            {
                _lobbyList.RemoveAt(_lobbyList.FindIndex(x => x.Id == lobbyId));
            }
            return RedirectToAction("TilslutLobby");
        }

    }
}