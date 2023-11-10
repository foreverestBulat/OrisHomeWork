using MyHttpServer.Model;
using MyHttpServerCopy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyHttpServerCopy.deserializeJsonRequestPOST
{
    public class DeserializeJsonRequestPOST
    {
        public string id { get; set; }
        public string classJson { get; set; }

        public object GetDeserializeJson()
        {
            if (id == "0")
            {
                var account = JsonSerializer.Deserialize<Account>(classJson);
                return account;
            }
                
            else if (id == "1")
            {
                Console.WriteLine(classJson);
                return JsonSerializer.Deserialize<Form>(classJson);
            }
            else 
                return null;    
        }
    }
}
