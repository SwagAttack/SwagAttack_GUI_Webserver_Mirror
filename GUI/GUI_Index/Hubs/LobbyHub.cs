using System;
using System.Threading.Tasks;
using Domain.Models;
using GUI_Index.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

// https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api

namespace GUI_Index.Hubs
{
    public class LobbyHub : Hub
    {
        
        //private HttpContext context = new DefaultHttpContext();
        
        /// <summary>
        /// Called by SignalR on connection to page
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
           await this.Clients.Caller.SendAsync("Connect");
        }

        /// <summary>
        /// called by Lobby.js
        /// </summary>
        /// <param name="username">The username of the user in the lobby</param>
        /// <param name="Lobbyname">The lobbyname of the lobby where user is</param>
        /// <returns></returns>
        public async Task OnConnectedUserAsync(string username, string Lobbyname)
        {
            //add user to group
            await Groups.AddAsync(Context.ConnectionId, Lobbyname);
            
            //send to others in the group
            await this.Clients.OthersInGroup(Lobbyname).SendAsync("onConnectedUser", username);

            //old
            //await this.Clients.Others.SendAsync("OnConnectedUser", username);
        }

        /// <summary>
        /// Called by Lobby.js
        /// </summary>
        /// <param name="user"> the user in the lobby that sends</param>
        /// <param name="LobbyName"> the lobby that the user is in</param>
        /// <param name="message"> the message the user wishes to send</param>
        /// <returns></returns>
        public async Task SendMessageAsync(string user,string LobbyName, string message)
        {
            await this.Clients.Group(LobbyName).SendAsync("ReceiveMessage", user, message);

            //old
            //await this.Clients.All.SendAsync("ReceiveMessage", user, message);

        }

        /*
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await this.Clients.All.SendAsync("Disconnect");
        }

        public async Task OnDisconnectedUserAsync(string username)
        {
            await this.Clients.Others.SendAsync("OnDisconnectedUser", username);
        }

        */

    }
}
