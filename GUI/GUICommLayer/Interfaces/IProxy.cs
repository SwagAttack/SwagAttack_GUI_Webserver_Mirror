using System;
using System.Threading.Tasks;

namespace GUICommLayer.Interfaces
{
    public interface IProxy<out T>
    {
        Task<Uri> CreateInstance();
        T RequestInstance();
    }
}