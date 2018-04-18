using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace GUI_Index.Hubs
{

    public class LobbyHub : Hub
    {
        
        private static LobbyNamesLog lobbies = new LobbyNamesLog();
        public async Task OpretLobbyAsync(string LobbyName)
        {
            await this.Clients.All.SendAsync("OprettetLobby", LobbyName);
            lobbies.logNewLobby(LobbyName);
        }

        public override async Task OnConnectedAsync()
        {
            //keep new clients updated on lobbies
            foreach (var VARIABLE in lobbies.LobbyNameList)
            {
                await this.Clients.Caller.SendAsync("OprettetLobby", VARIABLE);
            }

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

    public class LobbyNamesLog
    {
        public List<String> LobbyNameList = new List<string>();

        public void logNewLobby(string name)
        {
            LobbyNameList.Add(name);
        }
    }
}
