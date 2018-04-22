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
            this.Clients.Caller.SendAsync("Connect").Wait();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {

            this.Clients.All.SendAsync("Disconnect").Wait();
        }
        public async Task SendMessageAsync(string user,string message)
        {
            await this.Clients.All.SendAsync("ReceiveMessage",user, message);
            //x.LogNew(user, message);
        }

        public async Task OnConnectedUserAsync(string username)
        {
            await this.Clients.Others.SendAsync("OnConnectedUser", username);
        }

        public async Task OnDisconnectedUserAsync(string username)
        {
            await this.Clients.Others.SendAsync("OnDisconnectedUser", username);
        }
    }
}
