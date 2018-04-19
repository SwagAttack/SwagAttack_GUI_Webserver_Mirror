using System.Threading.Tasks;
using Domain.Interfaces;

namespace GUICommLayer.Interfaces
{
    public interface ILobbyProxy
    {
        Task<ILobby> CreateInstanceAsync(ILobby lobby, IUser user);
        Task<ILobby> RequestInstanceAsync(string lobbyId, IUser user);
    }
}