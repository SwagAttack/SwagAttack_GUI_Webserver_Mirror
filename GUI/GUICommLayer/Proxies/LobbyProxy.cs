using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using GUICommLayer.Interfaces;

namespace GUICommLayer.Proxies
{
    class LobbyProxy : IProxy<ILobby>
    {
        public Task<Uri> CreateInstance()
        {
            throw new NotImplementedException();
        }

        public ILobby RequestInstance()
        {
            throw new NotImplementedException();
        }
    }
}
