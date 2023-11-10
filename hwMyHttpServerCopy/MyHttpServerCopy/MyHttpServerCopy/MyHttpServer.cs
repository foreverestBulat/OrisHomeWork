using MyHttpServer.Handlers;
using MyHttpServer.Model;
using MyHttpServer.Services.EmailSender;
using MyHttpServerCopy.deserializeJsonRequestPOST;
using MyHttpServerCopy.Model;
using System.IO.Compression;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MyHttpServerCopy.CookieUse;

namespace MyHttpServer;

public class HttpServer
{
    private static Account[] accounts = new Account[] 
    { 
        new Account { Login = "bulat", Password = "subuh"},
        new Account { Login = "islam", Password = "bagav"}
    };

    private static List<Account> onlineAccounts = new List<Account>();
    internal HttpListener server;
    internal Task startServer;
    internal Task waitFinish;
    internal HttpListenerContext context;
    internal HttpListenerResponse response;
    internal CancellationTokenSource cts = new();
    internal HttpListenerRequest request;
    internal string path;                // = "C:/Users/Admin/Desktop/ОРИС/static/main.html";
    public AppSettingsConfig config;
    internal Cookie cookie;
    //string connectionString = @"Data Source=FOREVEREST;Initial Catalog=usersdb;Integrated Security=True";
    private int countRequests = 0;
    private int post = 0;
    private bool isAutificated = false;


    public HttpServer()
    {
        server = new HttpListener();
    }

    public void Start()
    {
        startServer = new Task(() => Run());
        waitFinish = new Task(() => Wait());
        startServer.Start();
        waitFinish.Start();

        Task.WaitAll(new Task[] { startServer, waitFinish });
    }

    private async void Run()
    {
        GetConfig("appsettings.json");
        server.Prefixes.Add($"{config.Address}:{config.Port}/"); //($"{config.Address}:{config.Port}/");        // "http://127.0.0.1:2323/"
        Console.WriteLine("Запуск сервера");
        server.Start();

        while (true)
        {
            context = await server.GetContextAsync();
            request = context.Request;
            response = context.Response;

            Handler staticFilesHandler = new StaticFileHandlers(config);
            Handler controllerHandler = new ControllerHandler();
            staticFilesHandler.Successor = controllerHandler;
            staticFilesHandler.HandleRequest(context);
            Console.WriteLine(request.Url.LocalPath);

            if (!(waitFinish.Status == TaskStatus.Running))
                break;

            Console.WriteLine("Запрос обработан");
            countRequests++;
        }
        server.Stop();
    }

    private void GetConfig(string filename)
    {
        if (File.Exists(filename))
        {

            using (var file = new FileStream("appsettings.json", FileMode.Open))
            {
                config = JsonSerializer.Deserialize<AppSettingsConfig>(file);
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            CheckExistFolderStatic();
        }
        else
        {
            throw new FileNotFoundException(filename);
        }
    }

    private void CheckExistFolderStatic()
    {
        if (!Directory.Exists(config.StaticPathFiles))
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), config.StaticPathFiles));
                Console.WriteLine("Была создана папка static", config.StaticPathFiles);
            }
            catch
            {
                Console.WriteLine("Невозможно создать папку");
            }
        }
    }

    private void Wait()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (input == "stop")
                break;
        }
    }

    private void Stop()
    {
        server.Stop();
    }
}






//Uri uri = new Uri("http://127.0.0.1:2323/");

//CookieContainer cookies = new CookieContainer();

//foreach (var cookieHeader in response.Headers.GetValues("Set-Cookie"))
//    // добавляем заголовки кук в CookieContainer
//    cookies.SetCookies(uri, cookieHeader);

//foreach (Cookie cookie in cookies.GetCookies(uri))
//    Console.WriteLine($"{cookie.Name}: {cookie.Value}");

// get cookie

//var cookie = request.Cookies;

//Cookie cookie1 = new Cookie("asd","sadf");
//cookie1.Expires = DateTime.Now.AddDays(1);
//response.Cookies.Add(cookie1);
//Console.WriteLine(System.String.Join(' ', cookie));


//var cookie = new Cookie("myCookie", "cookieValue");
//cookie.Expires = DateTime.Now.AddDays(1);
//response.SetCookie(cookie);
//response.AppendCookie(cookie);

//var cookie = new Cookie("myCookie", "cookieValue");
//cookie.Expires = DateTime.Now.AddDays(1);
//response.SetCookie(cookie);
//response.AppendCookie(cookie);

//response.Headers.Set("Set-Cookie", "true");
//Handler staticFilesHandler = new StaticFileHandlers(config);
//Handler controllerHandler = new ControllerHandler();
//staticFilesHandler.Successor = controllerHandler;
//staticFilesHandler.HandleRequest(context);




//string requestedPath = request.Url.LocalPath;
//Console.WriteLine(request.HttpMethod);

//if (request.HttpMethod == "POST")
//{
//    Console.WriteLine(requestedPath);
//    SendAnswer();
//}

//// Проверить, запрашивается ли файл CSS или изображение
//if (requestedPath.EndsWith(".css"))
//{
//    // Отправить файл CSS
//    SendCSSFile(requestedPath);
//}
//else if (requestedPath.StartsWith("/images/"))
//{
//    // Отправить изображение
//    SendImageFile(requestedPath);
//}
//else if (requestedPath.StartsWith("/scripts/"))
//{
//    //Console.WriteLine($"{requestedPath}");
//    SendScriptFile(requestedPath);
//}
//else if (requestedPath.EndsWith(".html") && isAutificated)
//{
//    // Отправка файл HTML
//    SendHTMLFile(requestedPath);
//}
//else //if () ////if (request.HttpMethod == "GET") //if (requestedPath.EndsWith("/") && countRequests == 0)
//{
//    SendFirstHTMLFile();
//}






//private void SendAnswer()
//{
//    Console.WriteLine("Отправить ответ");

//    var obj = GetObject();
//    if (obj == null)
//        return;
//    else if (obj is Account && accounts.Contains(obj))
//    {
//        onlineAccounts.Add((Account)obj);
//        var cookie = new Cookie(((Account)obj).Login, ((Account)obj).Password);
//        response.Cookies.Add(cookie);
//        isAutificated = true;
//    }
//    else if (obj is Form && post < 1)
//        SendFormMail((Form)obj);
//}

//private object GetObject()
//{
//    string dataString;
//    using (var reader = new StreamReader(request.InputStream))
//        dataString = reader.ReadToEnd();

//    if (dataString == null || dataString == "")
//        return null;

//    try
//    {
//        return JsonSerializer.Deserialize<DeserializeJsonRequestPOST>(dataString).GetDeserializeJson();
//    }
//    catch
//    {
//        return null;
//    }
//}

//private void SendFormMail(Form form)
//{
//    var subject = "Отправка на почту от Додо пиццы";
//    string body = $"{form.Name}\n" +
//                    $"{form.LastName}\n" +
//                    $"{form.BirthDay}\n" +
//                    $"{form.Number}";

//    var text = Uri.UnescapeDataString(Regex.Unescape(body));

//    ExistZipFile(config.StaticPathFiles, config.NameZipFile);

//    IEmailSenderService mail = new EmailSenderService(form.Name, config.EmailFrom);
//    mail.CreateMail(form.Mail, subject, text, $"{config.NameZipFile}.zip"); //config.EmailFrom
//    mail.SendMail(config.SmtpHost, config.SmtpPort, config.EmailPassword);

//    Console.WriteLine("Анкета отправлена на почту Додо");
//}

//private async void SendHTMLFile(string filePath)
//{
//    string fullPath = Path.Combine(Environment.CurrentDirectory, $"{config.StaticPathFiles}", filePath.TrimStart('/'));
//    //Console.WriteLine(fullPath);
//    if (File.Exists(fullPath))
//    {
//        byte[] fileBytes = File.ReadAllBytes(fullPath);
//        response.ContentType = "text/html";
//        using Stream outputStream = response.OutputStream;
//        response.ContentLength64 = fileBytes.Length;
//        await outputStream.WriteAsync(fileBytes);
//        await outputStream.FlushAsync();
//    }
//    else
//    {
//        response.StatusCode = 404;
//        response.ContentType = "text/plain; charset=utf-8";
//        var buffer = Encoding.UTF8.GetBytes("404 File Not Found - файл не найден");
//        response.ContentLength64 = buffer.Length;
//        await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
//        Console.WriteLine("File not founded");
//    }
//}

//private async void SendFirstHTMLFile()
//{
//    string fullPath = $"{config.StaticPathFiles}/html/authorization.html";
//    if (File.Exists(fullPath))
//    {
//        byte[] fileBytes = File.ReadAllBytes(fullPath);
//        response.ContentType = "text/html";
//        response.ContentLength64 = fileBytes.Length;
//        using Stream outputStream = response.OutputStream;
//        await outputStream.WriteAsync(fileBytes);
//        await outputStream.FlushAsync();
//    }
//    else
//    {
//        response.StatusCode = 404;
//        response.ContentType = "text/plain; charset=utf-8";
//        var buffer = Encoding.UTF8.GetBytes("404 File Not Found - файл не найден");
//        response.ContentLength64 = buffer.Length;
//        await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
//        Console.WriteLine("File not founded");
//    }
//}

//private async void SendScriptFile(string filePath)
//{
//    string fullPath = Path.Combine(Environment.CurrentDirectory, config.StaticPathFiles, filePath.TrimStart('/'));
//    //Console.WriteLine(fullPath);
//    if (File.Exists(fullPath))
//    {
//        byte[] fileBytes = File.ReadAllBytes(fullPath);
//        response.ContentType = "text/javascript";
//        response.ContentLength64 = fileBytes.Length;
//        using Stream outputStream = response.OutputStream;
//        await outputStream.WriteAsync(fileBytes);
//        await outputStream.FlushAsync();
//    }
//    else
//    {
//        response.StatusCode = 404;
//        response.ContentType = "text/plain; charset=utf-8";
//        var buffer = Encoding.UTF8.GetBytes("404 File Not Found - файл не найден");
//        response.ContentLength64 = buffer.Length;
//        await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
//        Console.WriteLine("File not founded");
//    }
//}

//private async void SendCSSFile(string filePath)
//{
//    string fullPath = Path.Combine(Environment.CurrentDirectory, config.StaticPathFiles, filePath.TrimStart('/'));
//    //Console.WriteLine(fullPath);
//    if (File.Exists(fullPath))
//    {
//        byte[] fileBytes = File.ReadAllBytes(fullPath);
//        response.ContentType = "text/css";
//        response.ContentLength64 = fileBytes.Length;
//        using Stream outputStream = response.OutputStream;
//        await outputStream.WriteAsync(fileBytes);
//        await outputStream.FlushAsync();
//    }
//    else
//    {
//        response.StatusCode = 404;
//        response.ContentType = "text/plain; charset=utf-8";
//        var buffer = Encoding.UTF8.GetBytes("404 File Not Found - файл не найден");
//        response.ContentLength64 = buffer.Length;
//        await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
//        Console.WriteLine("File not founded");
//    }
//}

//private string GetImageContentType(string imagePath)
//{
//    string extension = Path.GetExtension(imagePath).ToLower();
//    switch (extension)
//    {
//        case ".jpg":
//        case ".jpeg":
//            return "image/jpeg";
//        case ".png":
//            return "image/png";
//        case ".svg":
//            return "image/svg+xml";
//        default:
//            return "application/octet-stream"; // Если формат неизвестен, отправляем общий тип содержимого
//    }
//}

//private async void SendImageFile(string imagePath)
//{
//    string fullPath = Path.Combine(Environment.CurrentDirectory, "static", imagePath.TrimStart('/'));
//    //Console.WriteLine(fullPath);
//    if (File.Exists(fullPath))
//    {
//        byte[] imageBytes = File.ReadAllBytes(fullPath);
//        string contentType = GetImageContentType(fullPath);
//        response.ContentType = contentType;
//        response.ContentLength64 = imageBytes.Length;
//        using Stream outputStream = response.OutputStream;
//        await outputStream.WriteAsync(imageBytes);
//        await outputStream.FlushAsync();
//    }
//    else
//    {
//        response.StatusCode = 404;
//        response.ContentType = "text/plain; charset=utf-8";
//        var buffer = Encoding.UTF8.GetBytes("404 File Not Found - файл не найден");
//        response.ContentLength64 = buffer.Length;
//        await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
//        Console.WriteLine("File not founded");
//    }
//}


//private static bool ExistZipFile(string path, string putInsidePath)
//{
//    if (File.Exists($"{putInsidePath}.zip"))
//        return true;
//    CreateZipFile(path, putInsidePath);
//    return false;
//}

//public static void CreateZipFile(string path, string putInsidePath)
//{
//    ZipFile.CreateFromDirectory(@"static", $@"ZipFile.zip"); // @"static", @"ZipFile.zip"
//}

//public static void DeleteZipFile(string path)
//{
//    File.Delete(path);
//}