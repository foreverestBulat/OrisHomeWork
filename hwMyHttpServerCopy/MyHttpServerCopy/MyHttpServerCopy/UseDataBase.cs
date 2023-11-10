using MyHttpServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpServerCopy
{
    public static class UseDataBase
    {
        public static string ConnectionString;
        //public static void GetDateBase(string connectionString)
        //{

        //}

        public static bool FindAccount(Account account)
        {
            //IMyDataContext db = new MyDataContext(ConnectionString);

            //var accounts = db.Select<Account>();

            Console.WriteLine("--------------------------------------");
            //Console.WriteLine(String.Join(' ', accounts));
            Console.WriteLine(123);

            //return accounts.Contains(account);

            return true;
        }
    }
}
