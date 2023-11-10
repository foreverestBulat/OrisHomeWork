using MyHttpServer.Model;


namespace MyHttpServer;

public class Program
{
    public static void Main()
    {
        var server = new HttpServer();
        server.Start();


        //var account = new Account() {id = 3, Login = "ilnur", Password = "nigmat"};

        //IMyDataContext dataBase = new MyDataContext("localhost", "postgres", "postgres", "5432", "subuhankulov");
        //dataBase.Add(account);
        //dataBase.Update(account);
        //dataBase.Delete<Account>(3);
        //Console.WriteLine(String.Join(' ',dataBase.Select<Account>()));
        //Console.WriteLine(dataBase.SelectByID<Account>(1));



        //var mail = EmailSenderServis.CreateMail("Bulatic", "bulatsubuh@gmail.com", "bulatsubuh@gmail.com", "subject", "body");
        //EmailSenderServis.SendMail("127.0.0.1", 587, "bulatsubuh@gmail.com", "rwha zzei jwtv bvof", mail);

        //MailAddress from = new MailAddress("bulatsubuh@gmail.com", "Bulat");
        //MailAddress to = new MailAddress("islam.bagaviev.2014@mail.ru");
        //MailMessage m = new MailMessage(from, to);
        //m.Subject = "Отправка с помощью C#";
        //m.Body = "пашли в магаз пашли в магаз пашли в магаз";
        //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        //smtp.Credentials = new NetworkCredential("bulatsubuh@gmail.com", "rwha zzei jwtv bvof");
        //smtp.EnableSsl = true;
        //smtp.Send(m);
        //Console.WriteLine("Письмо отправлено");
    }
}