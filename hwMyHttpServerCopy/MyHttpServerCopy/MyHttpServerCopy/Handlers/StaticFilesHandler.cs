using MyHttpServer.tempOfExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpServer.Handlers;


public class StaticFileHandlers : Handler
{
    public AppSettingsConfig config;

    public StaticFileHandlers(AppSettingsConfig config)
    {
        this.config = config;
    }

    public override void HandleRequest(HttpListenerContext context)
    {
        //var request = context.Request;
        //using var response = context.Response;
        //var requestedPath = request.Url!.AbsolutePath;

        //var pathOfStaticFile = "/" == requestedPath ? $"{config.StaticPathFiles}/html/index.html" : $"{config.StaticPathFiles}{requestedPath}";

        ////var pathOfStaticFile = Path.Combine(config.StaticPathFiles, requestedPath.Trim('/'));
        //Console.WriteLine(pathOfStaticFile);

        var request = context.Request;
        using var response = context.Response;
        var requestedPath = request.Url!.AbsolutePath;

        if (requestedPath != null && requestedPath == "/")
        {
            requestedPath = "html/authorization.html";
        }

        var pathOfStaticFile = Path.Combine(config.StaticPathFiles, requestedPath.Trim('/'));

        if (requestedPath.Split('/').LastOrDefault().Contains('.') && request.HttpMethod == "GET")
        {
            Console.WriteLine("GET");
            var pattern = requestedPath?.Split('/')?.LastOrDefault()?.Split('.').LastOrDefault();
            if (File.Exists(pathOfStaticFile) && pattern != null)
            {
                response.ContentType = DictContentType.dictContentType[$".{pattern.ToLower()}"];
                using var fileStream = File.OpenRead(pathOfStaticFile);
                fileStream.CopyTo(response.OutputStream);
            }
            else
            {
                using var fileStream = File.OpenRead(Path.Combine(config.StaticPathFiles, "html/404.html"));
                fileStream.CopyTo(response.OutputStream);
            }
        }
        else if (Successor != null)
        {
            Console.WriteLine("---");
            Console.WriteLine(request.Url.LocalPath);
            Console.WriteLine("---");
            Successor.HandleRequest(context);
        }
    }
}