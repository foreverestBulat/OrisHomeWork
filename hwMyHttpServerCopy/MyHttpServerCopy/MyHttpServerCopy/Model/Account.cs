using MyHttpServerCopy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyHttpServer.Model
{
    public class Account
    {

        public int id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool isAutificated { get; set; }

        public override string ToString()
        {
            return $"Login: {Login}, Password: {Password}";
        }

        public override bool Equals(object? account)
        {
            return Login == ((Account)account).Login && Password == ((Account)account).Password;
        }
    }
}
