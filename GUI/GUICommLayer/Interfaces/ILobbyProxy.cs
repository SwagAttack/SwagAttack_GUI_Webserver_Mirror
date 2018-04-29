using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace GUICommLayer.Interfaces
{
    public interface ILobbyProxy
    {
        /// <summary>
        /// Creates an instance of lobby asynchronously and makes the user join the newly created lobby
        /// </summary>
        /// <param name="username">Username of requesting user</param>
        /// <param name="password">Password of requesting user</param>
        /// <returns></returns>
        Task<ILobby> CreateInstanceAsync(string lobbyId, string username, string password);

        /// <summary>
        /// Requests an instance of lobby asynchronously. Does NOT make a user join a lobby
        /// </summary>
        /// <param name="username">Username of requesting user</param>
        /// <param name="password">Password of requesting user</param>
        /// <returns></returns>
        Task<ILobby> RequestInstanceAsync(string lobbyId, string username, string password);

        /// <summary>
        /// Returns a list of all currently active Lobby Ids
        /// </summary>
        /// <param name="username">Username of requesting user</param>
        /// <param name="password">Password of requesting user</param>
        /// <returns></returns>
        Task<List<string>> GetAllLobbyIdsAsync(string username, string password);

        /// <summary>
        /// Makes a user join a lobby. Will return the updated lobby upon succes. Else null
        /// </summary>
        /// <param name="lobbyId">The lobby id of the lobby to join</param>
        /// <param name="username">The username of the user wishing to join the lobby</param>
        /// <param name="password">The password of the user wishing to join the lobby</param>
        /// <returns></returns>
        Task<ILobby> JoinLobbyAsync(string lobbyId, string username, string password);

        /// <summary>
        /// Makes a user leave a lobby. Will return the updated lobby upon succes. Else null
        /// </summary>
        /// <param name="lobbyId">The lobby id of the lobby to leave</param>
        /// <param name="username">The username of the user wishing to leave the lobby</param>
        /// <param name="password">The password of the user wishing to leave the lobby</param>
        /// <returns></returns>
        Task<ILobby> LeaveLobbyAsync(string lobbyId, string username, string password);



    }
}