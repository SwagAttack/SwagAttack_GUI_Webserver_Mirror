using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace GUICommLayer.Interfaces
{
    public interface IGameProxy
    {
	    Task<IGame> CreateInstanceAsync(string username, string password, string gameId, List<string> playesrList);
	    /// <summary>
	    /// Requests an instance of user asynchronously
	    /// </summary>
	    /// <param name="username"></param>
	    /// <param name="password"></param>
	    /// <returns></returns>
	    Task<IGamePlayerInfo> GetPlayerInfoAsync(string username, string password, string gameId);
	    Task<IGame> LeaveGameAsync(string username, string password, string gameId);
	}
}
