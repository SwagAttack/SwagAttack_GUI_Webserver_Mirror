using System.Net.Http;

namespace GUICommLayer.Interfaces
{
    public interface IHttpClient
    {
        HttpClient getInstance();
    }
}