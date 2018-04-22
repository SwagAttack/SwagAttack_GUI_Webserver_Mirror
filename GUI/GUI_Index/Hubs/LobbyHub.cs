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
            this.Clients.Others.SendAsync("Connect").Wait();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            this.Clients.Others.SendAsync("Disconnect").Wait();
        }
        public async Task SendMessage(string user,string message)
        {
            await this.Clients.All.SendAsync("ReceiveMessage",user, message);
            //x.LogNew(user, message);
        }

    }
}
