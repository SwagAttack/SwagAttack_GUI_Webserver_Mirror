using System;
using System.Collections.Generic;
using System.Text;

namespace TemporaryDomainLayer
{
    public interface IUser
    {
        string id { get; set; }

        string Username { get; set; }

        string GivenName { get; set; }

        string LastName { get; set; }

        string Password { get; set; }
    }
}
