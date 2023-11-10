using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace MyHttpServer.Services.EmailSender
{
    internal class EmailSenderService : IEmailSenderService
    {
        public string Name { get; set; }
        public MailAddress From { get; set; }
        public MailAddress To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public MailMessage Mail { get; set; }
        public Attachment Attachment { get; set; }
        public AppSettingsConfig config { get; set; }

        public EmailSenderService(string name)
        {
            using (var file = new FileStream("appsettings.json", FileMode.Open))
            {
                config = JsonSerializer.Deserialize<AppSettingsConfig>(file);
            }

            Name = name;
            From = new MailAddress(config.EmailFrom, name);
        }

        void IEmailSenderService.CreateMail(string emailTo, string subject, string body)
        {
            emailTo.Replace("%40", "@");
            string decodedEmail = HttpUtility.UrlDecode(emailTo);

            To = new MailAddress(decodedEmail);

            Mail = new MailMessage(From, To);
            Mail.BodyEncoding = Encoding.UTF8;
            Mail.Subject = Subject = subject;
            Mail.SubjectEncoding = Encoding.UTF8;
            Mail.Body = Body = body;
            Attachment = new Attachment($"{config.NameZipFile}.zip");
            Mail.Attachments.Add(Attachment);
        }

        void IEmailSenderService.SendMail()//string host, int smtpPort, string pass
        {
            SmtpClient smtpClient = new SmtpClient(config.SmtpHost, config.SmtpPort);
            smtpClient.Credentials = new NetworkCredential(From.User, config.EmailPassword);
            smtpClient.EnableSsl = true;
            smtpClient.SendMailAsync(Mail);
        }
    }
}
