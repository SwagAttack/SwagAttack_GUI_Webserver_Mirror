using System.Net.Http;
using System.Threading.Tasks;

namespace GUICommLayer.Interfaces
{
    public interface IHttpRequestBuilder
    {
        IHttpRequestBuilder AddUriPath(string path);
        IHttpRequestBuilder AddContent(object content);
        IHttpRequestBuilder AddUriQuery(string name, string value);
        IHttpRequestBuilder AddAuthentication(string username, string password);
        Task<HttpResponseMessage> SendAsync();
    }
}