using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using GUICommLayer.Interfaces;

namespace GUICommLayer.Proxies
{
    class UserProxy : IProxy<IUser>
    {
        public Task<Uri> CreateInstance()
        {
            throw new NotImplementedException();
        }

        public IUser RequestInstance()
        {
            throw new NotImplementedException();
        }
    }
}
