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
        //sessions
        private readonly IUserSession _userSession;
        //
        //private static List<ILobby> _lobbyList = new List<ILobby>();
        private LobbyProxy _lobbyProxy;
        private UserProxy _userproxy;

        public LobbyController(IUserProxy userProxy, ILobbyProxy lobbyProxy, IUserSession userSession)
        {
            _userproxy = userProxy as UserProxy;
            _lobbyProxy = lobbyProxy as LobbyProxy;
            _userSession = userSession;
        }
        [HttpGet]
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
                var currentUser = _userSession.User;

                //add lobby
                _lobbyProxy.CreateInstanceAsync(lobby.Id, currentUser.Username, currentUser.Password).Wait();

                LobbyViewModel returns = new LobbyViewModel();
                returns.Id = lobby.Id;
                returns.Admin = currentUser.Username;
                returns.Usernames.Add(currentUser.Username);

                return RedirectToAction("Lobby","Lobby",returns);

            }
            catch (ArgumentException)
            {
                return RedirectToAction("OpretLobby");
            }
            
        }
        [HttpGet]
        public IActionResult TilslutLobby()
        {
            //make new viewmodel
            TilslutLobbyViewModel returns = new TilslutLobbyViewModel();
            //get user
            var currentUser = _userSession.User;
            //get lobbylist
            List<string> lobbies = _lobbyProxy.GetAllLobbyIdsAsync(currentUser.Username, currentUser.Password).Result;

            foreach (var varLobby in lobbies)
            {
                returns.Lobbies.Add(varLobby);
            }
            return View(returns);
        }

        [HttpPost]
        public IActionResult TilslutLobby(LobbyViewModel model)
        {
            var currentuser = _userSession.User;
            ILobby concreateLobby = _lobbyProxy.JoinLobbyAsync(model.Id, currentuser.Username, currentuser.Password).Result;

            if (concreateLobby != null)
            {
                LobbyViewModel updatedModel = new LobbyViewModel();

                updatedModel.Id = concreateLobby.Id;
                updatedModel.Admin = concreateLobby.AdminUserName;
                updatedModel.Usernames = concreateLobby.Usernames.ToList();
                return RedirectToAction("Lobby", updatedModel);
            }
                return RedirectToAction("TilslutLobby");

        }
        [HttpGet]
        public IActionResult Lobby(LobbyViewModel model)
        {
            return View(model);
        }
        [HttpGet]
        public IActionResult ForladLobby(string lobbyId)
        {
            var currentUser = _userSession.User;
            var lobby = _lobbyProxy.LeaveLobbyAsync(lobbyId, currentUser.Username, currentUser.Password).Result;
            
            return RedirectToAction("TilslutLobby");
        }

    }
}