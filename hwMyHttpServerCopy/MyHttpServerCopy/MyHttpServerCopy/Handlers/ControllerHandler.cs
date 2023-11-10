using MyHttpServer.Model;
using MyHttpServer.Services.EmailSender;
using MyHttpServer.tempOfExtensions;
using MyHttpServerCopy.Controllers;
using MyHttpServerCopy.CookieUse;
using MyHttpServerCopy.deserializeJsonRequestPOST;
using MyHttpServerCopy.Model;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;


namespace MyHttpServer.Handlers;


public class ControllerHandler : Handler
{   
    private static bool isAutificated = false;
    private static AccountController accountController = new AccountController(); 

    public override void HandleRequest(HttpListenerContext context)
    {
        try
        {
            var request = context.Request;
            using var response = context.Response;

            if (request.HttpMethod == "POST")
            {
                Console.WriteLine("POST");

                var obj = GetObject(request);
                if (obj == null)
                {
                    Console.WriteLine(request.Url.AbsolutePath);
                    SendAnswer(request.Url.AbsolutePath, response);
                    return;
                }

                SendAnswer(obj, response);
            }
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static object GetObject(HttpListenerRequest request)
    {
        string dataString;
        using (var reader = new StreamReader(request.InputStream))
            dataString = reader.ReadToEnd();

        if (dataString == null || dataString == "")
            return null;

        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<DeserializeJsonRequestPOST>(dataString).GetDeserializeJson();
        }
        catch
        {
            return null;
        }
    }

    private static void SendAnswer(object obj, HttpListenerResponse response)
    {
        switch (obj)
        {
            case null:
                {
                    response.ContentType = DictContentType.dictContentType[".html"];
                    using var reader = new StreamReader("static/html/authorization.html");
                    var buffer = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                    
                    break;
                }
            case string filePath:
                {
                    try
                    {
                        if (isAutificated)
                        {
                            response.ContentType = DictContentType.dictContentType[".html"];
                            using var reader = new StreamReader($"static/{filePath}");
                            var buffer = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                        else
                        {
                            response.ContentType = DictContentType.dictContentType[".html"];
                            using var reader = new StreamReader($"static/html/authorization.html");
                            var buffer = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                }
            case Account account:
                {
                    if (accountController.GetAccounsAll().Contains(account))
                    {
                        isAutificated = true;
                        if (CookieUse.IsExistCookieByProps((Account)obj))
                            CookieUse.CreateCookieForAccount((Account)obj, response);
                    }
                    break;
                }
            case Form:
                {
                    SendFormMail((Form)obj);
                    break;
                }

        }
    }

    private static void SendFormMail(Form form)
    {
        var subject = "Отправка на почту от Додо пиццы";
        string body = $"{form.Name}\n" +
                        $"{form.LastName}\n" +
                        $"{form.BirthDay}\n" +
                        $"{form.Number}";

        var text = Uri.UnescapeDataString(Regex.Unescape(body));

        IEmailSenderService mail = new EmailSenderService(form.Name);
        mail.CreateMail(form.Mail, subject, text);
        mail.SendMail();

        Console.WriteLine("Анкета отправлена на почту Додо");
    }
}




//private static bool ExistZipFile(string path, string putInsidePath)
//{
//    if (File.Exists($"{putInsidePath}.zip"))
//        return true;
//    CreateZipFile(path, putInsidePath);
//    return false;
//}

//private static void CreateZipFile(string path, string putInsidePath)
//{
//    ZipFile.CreateFromDirectory(@"static", $@"ZipFile.zip");
//}

//var strParams = context?.Request.Url!
//    .Segments
//    .Skip(1)
//    .Select(s => s.Replace("/", ""))
//    .ToArray();

//Console.WriteLine(strParams.Length);
//if (strParams!.Length < 1)
//    throw new ArgumentNullException("the number of lines in the query string is less than two!");

//using var streamReader = new StreamReader(context!.Request.InputStream);
//var tempOfData = streamReader.ReadToEnd();
//string[] formData = new[] { "" };

//if (!String.IsNullOrEmpty(tempOfData))
//{
//    var currentOfUserData = tempOfData?.Split('&');
//    formData = new string[] { WebUtility.UrlDecode(currentOfUserData[0][6..]), currentOfUserData[1][9..] };
//}

//var controllerName = strParams[0];
//var methodName = strParams[1];
//var assembly = Assembly.GetExecutingAssembly();

//var controller = assembly.GetTypes()
//    .Where(t => Attribute.IsDefined(t, typeof(ControllerAttribute)))
//    .FirstOrDefault(c => ((ControllerAttribute)Attribute.GetCustomAttribute(c, typeof(ControllerAttribute))!)
//        .ControllerName.Equals(controllerName, StringComparison.OrdinalIgnoreCase));

//var list = controller?.GetMethods()
//    .Select(method => new { Name = method.Name, Attributes = method.GetCustomAttributes() });

//var method = controller?.GetMethods()
//    .Where(x => x.GetCustomAttributes(true)
//        .Any(attr => attr.GetType().Name.Equals($"{context.Request.HttpMethod}Attribute",
//            StringComparison.OrdinalIgnoreCase)))
//    .FirstOrDefault(m => m.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase));

//var queryParams = new object[] { };

//if (formData.Length > 1)
//{
//    queryParams = method?.GetParameters()
//    .Select((p, i) => Convert.ChangeType(formData[i], p.ParameterType))
//    .ToArray();
//}

//method?.Invoke(Activator.CreateInstance(controller), queryParams);







//{



//    public class ControllerHandler : Handler
//    {
//        public override void HandleRequest(HttpListenerContext context)
//        {
//            //request.Url.LocalPath

//            //try
//            //{
//            //    string[] strParams = context.Request.Url
//            //        .Segments
//            //        .Skip(2)
//            //        .Select(s => s.Replace("/", ""))
//            //        .ToArray();

//            //    using var streamReader = new StreamReader();

//            //    var assembly = Assembly.GetExecutingAssembly();

//            //    var controller = assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(HttpController))).FirstOrDefault(c => c.Name.ToLower() == controllerName.ToLower());
//            //}

//            if (context.Request.HttpMethod == "HOST")
//            {

//            }


//            if (context.Request.Url.AbsolutePath.EndsWith(".html"))
//            {


//                // завершение выполнения запроса;
//            }
//            // передача запроса дальше по цепи при наличии в ней обработчиков
//            else if (Successor != null)
//            {
//                Successor.HandleRequest(context);
//            }

//        }

//        private void SendMail(AppSettingsConfig config)
//        {
//            var type = typeof(EmailSenderService);

//            var i = type.GetInterface("IEmailSenderService");

//            ConstructorInfo info = info = type.GetConstructor(new Type[] { });

//            object obj = info.Invoke(new object[] { });

//            MailMessage mail = new MailMessage();

//            foreach (var property in i.GetProperties())
//            {
//                if (property.Name == "From")
//                {
//                    property.SetValue(obj, config.EmailFrom);
//                }
//                if (property.Name == "To")
//                {
//                    property.SetValue(obj, config.EmailTo);
//                }
//                if (property.Name == "Mail")
//                {
//                    property.SetValue(obj, new MailMessage());
//                }
//            }
//        }

//        private AppSettingsConfig GetConfig(string filename)
//        {
//            if (File.Exists(filename))
//            {

//                AppSettingsConfig config;

//                using (var file = new FileStream("appsettings.json", FileMode.Open))
//                {
//                    config = System.Text.Json.JsonSerializer.Deserialize<AppSettingsConfig>(file);
//                }

//                if (config == null)
//                {
//                    throw new ArgumentNullException(nameof(config));
//                }

//                CheckExistFolderStatic(config);

//                return config;
//            }
//            else
//            {
//                throw new FileNotFoundException(filename);
//            }
//        }

//        private void CheckExistFolderStatic(AppSettingsConfig config)
//        {
//            if (!Directory.Exists(config.StaticPathFiles))
//            {
//                try
//                {
//                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), config.StaticPathFiles));
//                    Console.WriteLine("Была создана папка static", config.StaticPathFiles);
//                }
//                catch
//                {
//                    Console.WriteLine("Невозможно создать папку");
//                }
//            }
//        }
//    }
//}

//var strParams = context?.Request.Url!
//    .Segments
//    .Skip(1)
//    .Select(s => s.Replace("/", ""))
//    .ToArray();

//Console.WriteLine(strParams.Length);
//if (strParams!.Length < 1)
//    throw new ArgumentNullException("the number of lines in the query string is less than two!");

//using var streamReader = new StreamReader(context!.Request.InputStream);
//var tempOfData = streamReader.ReadToEnd();
//string[] formData = new[] { "" };

//if (!String.IsNullOrEmpty(tempOfData))
//{
//    var currentOfUserData = tempOfData?.Split('&');
//    formData = new string[] { WebUtility.UrlDecode(currentOfUserData[0][6..]), currentOfUserData[1][9..] };
//}

//var controllerName = strParams[0];
//var methodName = strParams[1];
//var assembly = Assembly.GetExecutingAssembly();

//var controller = assembly.GetTypes()
//    .Where(t => Attribute.IsDefined(t, typeof(ControllerAttribute)))
//    .FirstOrDefault(c => ((ControllerAttribute)Attribute.GetCustomAttribute(c, typeof(ControllerAttribute))!)
//        .ControllerName.Equals(controllerName, StringComparison.OrdinalIgnoreCase));

//var list = controller?.GetMethods()
//    .Select(method => new { Name = method.Name, Attributes = method.GetCustomAttributes() });

//var method = controller?.GetMethods()
//    .Where(x => x.GetCustomAttributes(true)
//        .Any(attr => attr.GetType().Name.Equals($"{context.Request.HttpMethod}Attribute",
//            StringComparison.OrdinalIgnoreCase)))
//    .FirstOrDefault(m => m.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase));

//var queryParams = new object[] { };

//if (formData.Length > 1)
//{
//    queryParams = method?.GetParameters()
//    .Select((p, i) => Convert.ChangeType(formData[i], p.ParameterType))
//    .ToArray();
//}

//method?.Invoke(Activator.CreateInstance(controller), queryParams);





//string[] strParams = context.Request.Url
//    .Segments
//    .Skip(2)
//    .Select(s => s.Replace("/", ""))
//    .ToArray();

//var assembly = Assembly.GetExecutingAssembly();

//var controller = assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(HttpController))).FirstOrDefault(c => c.Name.ToLower() == controllerName.ToLower());

//if (controller == null) return false;

//var test = typeof(HttpController).Name;
//var method = controller.GetMethods().Where(t => t.GetCustomAttributes(true)
//.Any(attr => attr.GetType().Name == $"Http{_httpContext.Request.HttpMethod}"))
//.FirstOrDefault();

//if (method == null) return false;

//object[] queryParams = method.GetParameters()
//.Select((p, i) => Convert.ChangeType(strParams[i], p.ParameterType))
//.ToArray();

//var ret = method.Invoke(Activator.CreateInstance(controller), queryParams);


//            // некоторая обработка запроса
//            string[] strParams = context.Request.Url
//                .Segments
//                .Skip(2)
//                .Select(s => s.Replace("/", ""))
//                .ToArray();

//            var assembly = Assembly.GetExecutingAssembly();

////            var controller = assembly.GetTypes()
////.Where(t => Attribute.IsDefined(t, typeof(HttpController)))
////.FirstOrDefault(c => (((HttpController)Attribute.GetCustomAttribute(c, typeof(HttpController))!)!)
////.ControllerName.Equals(controllerName, StringComparison.OrdinalIgnoreCase));

//            //var controller = assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(HttpController))).FirstOrDefault(c => c.Name.ToLower() == controllerName.ToLower());

//            if (context.Request.Url.AbsolutePath.EndsWith(".html"))
//            {
//                // завершение выполнения запроса;
//            }
//            // передача запроса дальше по цепи при наличии в ней обработчиков
//            else if (Successor != null)
//            {
//                Successor.HandleRequest(context);
//            }