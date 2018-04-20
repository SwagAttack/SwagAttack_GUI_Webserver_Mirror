using System.Net.Http;

namespace GUICommLayer.Interfaces
{
    public interface IClientWrapper
    {
        HttpClient GetInstance();
    }
}