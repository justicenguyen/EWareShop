using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public static class SessionAndKookies
    {
        public static T GetSession<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }


        public static void SetSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static void SetKookies(this IResponseCookies cookies, string key, object value)
        {
            CookieOptions options = new Microsoft.AspNetCore.Http.CookieOptions();
            options.Expires = DateTime.Now.AddSeconds(100);
            cookies.Append(key, JsonConvert.SerializeObject(value), options);
        }

        public static T GetKookies<T>(this IRequestCookieCollection cookies, string key)
        {
            var data = cookies[key];
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
