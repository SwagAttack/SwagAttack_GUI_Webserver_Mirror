using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using GUICommLayer.Interfaces;
using GUICommLayer.Proxies.Utilities;

namespace GUICommLayer.Proxies
{
	class GameProxy : IGameProxy
	{
		private string _apiPath = "/api/Game";
		private readonly IHttpRequestFactory _requestFactory;
		public GameProxy(IHttpRequestFactory requestFactory)
		{
			_requestFactory = requestFactory;
		}

		public async Task<IGame> CreateInstanceAsync(string username, string password, string gameId, List<string> playesrList)
		{
			var request = _requestFactory.Post(_apiPath + "/Create").AddAuthentication(username, password)
				.AddAuthentication(username, password)
				.AddUriQuery("GameId", gameId)
				.AddContent(playesrList);
			var response = await request.SendAsync();

			return response.IsSuccessStatusCode ? response.ReadBodyAsType<IGame>() : null;
		}

		public async Task<IGamePlayerInfo> GetPlayerInfoAsync(string username, string password, string gameId)
		{
			var request = _requestFactory.Get(_apiPath  + gameId)
				.AddAuthentication(username, password);
			var response = await request.SendAsync();
			return response.IsSuccessStatusCode ? response.ReadBodyAsType<IGamePlayerInfo>() : null;
		}

		public async Task<IGame> LeaveGameAsync(string username, string password, string gameId)
		{
			var request = _requestFactory.Post(_apiPath +"/Leave")
				.AddAuthentication(username, password)
				.AddUriQuery("GameId", gameId );

			var response = await request.SendAsync();
			
			return response.IsSuccessStatusCode ? response.ReadBodyAsType<IGame>() : null;
		}
	}
}
	