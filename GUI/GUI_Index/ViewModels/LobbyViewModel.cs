using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUI_Index.ViewModels
{
    public class LobbyViewModel
    {
        public string Id { get; set; }
        public List<string> Usernames { get; set; } = new List<string>();
        public string Admin { get; set; }
    }
}
