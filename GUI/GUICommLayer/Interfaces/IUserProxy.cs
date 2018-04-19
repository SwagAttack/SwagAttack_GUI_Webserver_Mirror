using System;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace GUICommLayer.Interfaces
{
    public interface IUserProxy
    {
        Task<IUser> CreateInstanceAsync(IUser user);
        Task<IUser> RequestInstanceAsync(string username, string password);
    }
}