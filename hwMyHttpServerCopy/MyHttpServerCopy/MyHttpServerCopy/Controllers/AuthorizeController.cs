using MyHttpServer.Attribuete;
using MyHttpServer.Attribuets;
using MyHttpServer.Model;
using MyHttpServer.Services.EmailSender;
using MyHttpServerCopy.CookieUse;
using MyHttpServerCopy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MyHttpServer.Controller
{

    [Controller("Authorize")]
    public class AuthorizeController
    {

        [Post("SendToEmail")]
        public void SendToEmail(AppSettingsConfig config, string emailTo, string passwordFromUser)
        {
            //new EmailSenderService().SendEmail(emailFromUser, passwordFromUser, "");
            IEmailSenderService mail = new EmailSenderService(config.EmailFrom);
            string subject = "DODO PIZZA";
            string body = $@"
                    <!doctype html>
                    <html lang=""en"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport""
                              content=""width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0"">
                        <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
                        <style>
                            *{{
                                padding: 0;
                                margin: 0;
                                box-sizing: border-box;
                            }}

                            .message-card{{
                                background-color: black;
                                max-width:600px;
                                height: 300px;
                                color: white;
                                padding: 10px;
                            }}

                            .message-card h1 {{
                                text-align: center;
                                margin-bottom: 20px;
                            }}

                            .message-card p {{
                                font-size: 19px;
                                line-height: 50px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class=""message-card"">
                            <h1>Hello from Battle.net</h1>
                            <p>Your email: {config.EmailFrom}</p>
                            <footer>
                                <p>With love, battle.net and its creator Bulat 11-208</p>
                            </footer>
                        </div>
                    </body>
                    </html>
                ";


            mail.CreateMail(emailTo, subject, body);
            mail.SendMail();

            Console.WriteLine("Email has been sent.");
        }

        [Get("GetEmailList")]
        public string GetEmailList()
        {
            var htmlCode = "<h1>You are open GetEmailList method</h1>";
            return htmlCode;
        }

        [Get("GetAccountsList")]
        public Account[] GetAccountsList()
        {
            var accounts = new[]
            {
            new Account(){Login = "email-1", Password = "password-1"},
            new Account(){Login = "email-2", Password = "password-2"},
        };

            return accounts;
        }

        [Post("Login")]
        public void Login(string login, string password)
        {
            var accounts = new[]
            {
                new Account(){Login = "email-1", Password = "password-1"},
                new Account(){Login = "email-2", Password = "password-2"},
            };

            //CookieUse.CreateCookieForAccount(accounts[0], response);
        }
    }
}
