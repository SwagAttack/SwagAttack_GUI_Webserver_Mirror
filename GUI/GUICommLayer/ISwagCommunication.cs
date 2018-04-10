using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces;
using Models.User;

namespace GUICommLayer
{
    public interface ISwagCommunication
    {
        Task<Uri> CreateUserAsync(IUser user);
        Task<User> GetUserAsync(string username, string password);
        Task<HttpStatusCode> DeleteProductAsync(string username);
    }
}
