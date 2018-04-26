using GUICommLayer.Proxies.Utilities;

namespace GUICommLayer.Interfaces
{
    public interface IHttpRequestFactory
    {
        HttpRequestBuilder Get(string uri);
        HttpRequestBuilder Post(string uri, object obj = null);
        HttpRequestBuilder Put(string uri, object obj = null);
        HttpRequestBuilder Delete(string uri);
    }
}