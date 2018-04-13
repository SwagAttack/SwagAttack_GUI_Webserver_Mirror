using System;
using System.Net;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace GUICommLayer
{
    public interface ISwagCommunication
    {
        Task<Uri> CreateUserAsync(IUser user);
        Task<User> GetUserAsync(string username, string password);
        Task<HttpStatusCode> DeleteProductAsync(string username);
    }
}
