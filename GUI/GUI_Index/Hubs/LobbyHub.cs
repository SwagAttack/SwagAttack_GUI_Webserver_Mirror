using System;
using System.Threading.Tasks;
using Domain.Models;
using GUI_Index.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace GUI_Index.Hubs
{
    public class LobbyHub : Hub
    {
        
        private HttpContext context = new DefaultHttpContext();
        
        public override async Task OnConnectedAsync()
        {
            await this.Clients.All.SendAsync("Connect");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await this.Clients.Others.SendAsync("Disconnect");
        }

        //when navigating to the lobby, join a lobby group.
        public async Task JoinRoom(string LobbyName)
        {
            await this.Groups.AddAsync(Context.ConnectionId, LobbyName);
        }

        public async Task LeaveRoom(string LobbyName)
        {
            await this.Groups.RemoveAsync(Context.ConnectionId, LobbyName);
        }

    }
}
