using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

//from: https://benjii.me/2016/07/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core/

namespace GUI_Index.Session
{

    public interface IUserSession
    {
        User User { get; set; }

    }

    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public User User
        {
            get => _httpContextAccessor.HttpContext.Session.GetObjectFromJson<User>("user");
            set => _httpContextAccessor.HttpContext.Session.SetObjectAsJson("user", value);
        }
    }

    //public static class SessionExtensions
    //{
    //    public static User GetObjectFromJson<User>(
    //        this ISession sesson, string json) where User : new()
    //    {
    //        return new User(); // Dummy extension method just to test OP's code
    //    }
    //}

    public static class SessionExtension
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
