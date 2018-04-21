using System;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace GUICommLayer.Interfaces
{
    public interface IUserProxy
    {
        /// <summary>
        /// Creates an instance of user asynchronously
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IUser> CreateInstanceAsync(IUser user);
        /// <summary>
        /// Requests an instance of user asynchronously
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<IUser> RequestInstanceAsync(string username, string password);
    }
}