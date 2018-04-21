using System.Threading.Tasks;
using Domain.Interfaces;

namespace GUICommLayer.Interfaces
{
    public interface ILobbyProxy
    {
        /// <summary>
        /// Creates an instance of lobby asynchronously. 
        /// </summary>
        /// <param name="lobby"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ILobby> CreateInstanceAsync(ILobby lobby, IUser user);

        /// <summary>
        /// Requests an instance of lobby asynchronously
        /// </summary>
        /// <param name="lobbyId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ILobby> RequestInstanceAsync(string lobbyId, IUser user);
    }
}