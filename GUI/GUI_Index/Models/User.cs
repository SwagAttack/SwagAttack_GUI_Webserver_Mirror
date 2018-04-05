using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GUI_Index.Interfaces;

namespace GUI_Index.Models
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
