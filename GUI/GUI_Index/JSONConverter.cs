using System;
using GUI_Index.Models;
using Newtonsoft.Json;

namespace GUI_Index
{
    public class JSONConverter
    {

        public string newUser(User newJas)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(newJas);
            return jsonString;
        }

        public string logInUser(User login)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(login);
            return jsonString;
        }
    }

}
