using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies.Utilities;

namespace GUICommLayer.Proxies
{
    public class LobbyProxy : ILobbyProxy
    {
        private readonly IHttpRequestFactory _requestFactory;
        public LobbyProxy(IHttpRequestFactory requestFactory)
        {
            _requestFactory = requestFactory;
        }
        public async Task<ILobby> CreateInstanceAsync(string lobbyId, string username, string password)
        {

            var request = _requestFactory.Post("/api/Lobby/Create").AddAuthentication(username, password)
                .AddUriQuery("lobbyId", lobbyId);
            var response = await request.SendAsync();
            return response.IsSuccessStatusCode ? response.ReadBodyAsType<Lobby>() : null;
        }

        public async Task<ILobby> RequestInstanceAsync(string lobbyId, string username, string password)
        {
            var request = _requestFactory.Get("/api/Lobby").AddAuthentication(username, password)
                .AddUriQuery("lobbyId", lobbyId);
            var response = await request.SendAsync();
            return response.IsSuccessStatusCode ? response.ReadBodyAsType<Lobby>() : null;
        }

        public async Task<List<string>> GetAllLobbyIdsAsync(string username, string password)
        {
            var request = _requestFactory.Get("/api/Lobby").AddAuthentication(username, password);
            var response = await request.SendAsync();
            return response.IsSuccessStatusCode ? response.ReadBodyAsType<List<string>>() : null;
        }

        public async Task<ILobby> JoinLobbyAsync(string lobbyId, string username, string password)
        {
            var request = _requestFactory.Post("/api/Lobby/Join").AddAuthentication(username, password)
                .AddUriQuery("lobbyId", lobbyId);
            var response = await request.SendAsync();
            return response.IsSuccessStatusCode ? response.ReadBodyAsType<Lobby>() : null;
        }

        public async Task<ILobby> LeaveLobbyAsync(string lobbyId, string username, string password)
        {
            var request = _requestFactory.Post("/api/Lobby/Leave").AddAuthentication(username, password)
                .AddUriQuery("lobbyId", lobbyId);
            var response = await request.SendAsync();
            return response.IsSuccessStatusCode ? response.ReadBodyAsType<Lobby>() : null;
        }
    }
}
