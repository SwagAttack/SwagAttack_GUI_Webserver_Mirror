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
        //session
        private readonly IUserSession _userSession;
        
        //proxies for the api
        private ILobbyProxy _lobbyProxy;
        private IUserProxy _userproxy;
        /// <summary>
        /// setup everything for the LobbyController
        /// </summary>
        /// <param name="userProxy">Instance of user proxy</param>
        /// <param name="lobbyProxy">Instance of Lobby proxy</param>
        /// <param name="userSession">Instance of user session</param>
        public LobbyController(IUserProxy userProxy, ILobbyProxy lobbyProxy, IUserSession userSession)
        {
            _userproxy = userProxy;
            _lobbyProxy = lobbyProxy;
            _userSession = userSession;
        }

        /// <summary>
        /// Action that goes to opret lobby view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult OpretLobby()
        {
            return View("OpretLobby");
        }
        /// <summary>
        /// Action that makes a lobby
        /// </summary>
        /// <param name="lobby">the lobbyviewmodel sent from csthml</param>
        /// <returns>succes goes to lobby, failure goes back to opretlobby</returns>
        [HttpPost]
        public IActionResult OpretLobby(LobbyViewModel lobby)
        {
            
            //find brugeren der har lavet lobby
            var currentUser = _userSession.User;

            //add lobby
            ILobby lobbyReturn = _lobbyProxy.CreateInstanceAsync(lobby.Id, currentUser.Username, currentUser.Password).Result;
            if (lobbyReturn != null)
            {
                LobbyViewModel returns = new LobbyViewModel();
                returns.Id = lobby.Id;
                returns.Admin = currentUser.Username;
                returns.Usernames.Add(currentUser.Username);

                return RedirectToAction("Lobby", returns);

            }
            else
            {
                return RedirectToAction("LogInd", "Home");
            }

        }
        /// <summary>
        /// Goes to tilslut lobby, also gets the list of active lobbies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TilslutLobby()
        {
            //make new viewmodel
            TilslutLobbyViewModel returns = new TilslutLobbyViewModel();
            //get user
            var currentUser = _userSession.User;
            //get lobbylist
            List<string> lobbies = _lobbyProxy.GetAllLobbyIdsAsync(currentUser.Username, currentUser.Password).Result;

            if (lobbies != null)
            {
                foreach (var varLobby in lobbies)
                {
                    returns.Lobbies.Add(varLobby);
                }
                return View(returns);
            }

            return RedirectToAction("LogInd", "Home");
        }

        /// <summary>
        /// Goes to selected lobby, posted by the tilslut lobby view
        /// </summary>
        /// <param name="model">sent from tilslutlobby.cshtml</param>
        /// <returns>sucess goes to the lobby, failure goes back to tilslutlobby</returns>
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
        /// <summary>
        /// goes to lobby, makes sure reconnect works too
        /// </summary>
        /// <param name="model">the concreate lobby user entered</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Lobby(LobbyViewModel model)
        {
            //get the user
            var currentUser = _userSession.User;

            //get the concreate lobby again
            ILobby thisLobby = _lobbyProxy.RequestInstanceAsync(model.Id, currentUser.Username, currentUser.Password).Result;

            //make the model
            LobbyViewModel thisModel = new LobbyViewModel();
            thisModel.Admin = thisLobby.AdminUserName;
            thisModel.Id = thisLobby.Id;
            thisModel.Usernames = thisLobby.Usernames.ToList();

            return View(thisModel);
        }

        /// <summary>
        /// When exiting lobby
        /// </summary>
        /// <param name="model">the lobby that got exited</param>
        /// <returns>back to tilslut lobby</returns>
        [HttpGet]
        public IActionResult ForladLobby(LobbyViewModel model)
        {
            var currentUser = _userSession.User;
            var lobby = _lobbyProxy.LeaveLobbyAsync(model.Id, currentUser.Username, currentUser.Password).Result;
            
            return RedirectToAction("TilslutLobby");
        }

    }
}