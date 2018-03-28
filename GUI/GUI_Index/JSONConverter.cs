using System;
using GUI_Index.Models;
using Newtonsoft.Json;

namespace GUI_Index
{
    public class JSONConverter
    {

        public string NewUser(User newJas)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(newJas);
            return jsonString;
        }

        public string LogInUser(User login)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(login);
            return jsonString;
        }
    }

}
