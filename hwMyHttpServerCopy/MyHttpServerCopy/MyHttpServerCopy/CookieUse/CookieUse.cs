using MyHttpServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpServerCopy.CookieUse
{
    public class CookieUse
    {
        public static Uri domain = new Uri("http://127.0.0.1:2323/");
        public static CookieContainer container = new CookieContainer();
        public static void CreateCookieForAccount(Account account, HttpListenerResponse response)
        {
            var cookie = new Cookie(account.Login, account.Password);
            response.Cookies.Add(cookie);
            Console.WriteLine("Cookies has been added.");
        }

        public static bool IsExistCookieByProps(Account account)
        {
            var cookies = container.GetAllCookies();
            foreach (Cookie cookie in cookies)
            {
                Console.WriteLine(cookie.Name);
            }

            var existingCookie = cookies.Where(acc => acc.Name == account.Login && acc.Value == account.Password);
            return existingCookie.Count() > 0;
        }
    }
}
