using GUICommLayer.Proxies.Utilities;

namespace GUICommLayer.Interfaces
{
    public interface IHttpRequestFactory
    {
        string BaseAddress { get; }
        IHttpRequestBuilder Get(string path);
        IHttpRequestBuilder Post(string path, object obj = null);
        IHttpRequestBuilder Put(string path, object obj = null);
        IHttpRequestBuilder Delete(string path);
    }
}