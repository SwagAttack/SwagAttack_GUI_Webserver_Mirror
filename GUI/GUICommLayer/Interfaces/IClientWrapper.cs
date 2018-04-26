using System.Net.Http;

namespace GUICommLayer.Interfaces
{
    public interface IClientWrapper
    {
        /// <summary>
        /// Singleton -> returns of system.net.http.httpclient
        /// </summary>
        /// <returns></returns>
        HttpClient GetInstance(string uri = null);
    }
}