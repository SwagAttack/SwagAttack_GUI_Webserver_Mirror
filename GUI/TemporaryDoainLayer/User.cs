using System;

namespace TemporaryDomainLayer
{
   

    public class User : IUser
    {
        public string id { get; set; }

        public string Username { get; set; }

        public string GivenName { get; set; }

        public string LastName { get; set; }
        
        public string Password { get; set; }
    }

}
