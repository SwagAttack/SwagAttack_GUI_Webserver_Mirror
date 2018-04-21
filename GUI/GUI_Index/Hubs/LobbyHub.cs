using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace GUI_Index.Hubs
{
    public class LobbyHub : Hub
    {

        // private HttpContext context = new DefaultHttpContext();

        public override async Task OnConnectedAsync()
        {
            await this.Clients.All.SendAsync("Connect");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //tell all that somebody disconnected, refresh pages.
            Clients.All.SendAsync("Disconnect").Wait();
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
