using MyBlogProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlogProject.UI.Models
{
    public class CurrentSession //Session Helper
    {
        public static User User
        {
            get
            {
                return Get<User>("login");
            }
        }

        public static void Set<T>(string key, T obj) //verilen key'e göre oturum açma.
        {
            HttpContext.Current.Session[key] = obj;
        }

        public static T Get<T>(string key) //verilen key'e göre oturum bilgisi getirme.
        {
            if (HttpContext.Current.Session[key] != null)
            {
                return (T)HttpContext.Current.Session[key];
            }
            return default(T);
        }

        public static void Remove(string key) // verilen key'e göre oturum silme.
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        public static void Clear(string key) // açık olan tüm oturumları silme.
        {
            HttpContext.Current.Session.Clear();
        }
    }
}