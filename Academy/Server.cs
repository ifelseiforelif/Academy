using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json;
using Academy.DTO;

namespace Academy;

internal class Server
{
    readonly string _HOST = "http://127.0.0.1:8080/";
    public async Task RunServer(Academy academy)
    {
        HttpListener server = new HttpListener();
        server.Prefixes.Add(_HOST);
        server.Start();
        Console.WriteLine($"Server has been started {_HOST}");
        while (true)
        {
            try
            {
                HttpListenerContext ctx = await server.GetContextAsync();
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse res = ctx.Response;
                if (req.HttpMethod == "POST" && req.Url?.AbsolutePath == "/")
                {
                    string body = string.Empty;
                    using(var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        body = await reader.ReadToEndAsync();
                        var groupDto = JsonSerializer.Deserialize<GroupDTO>(body);
                        if(groupDto!=null)
                        {
                            bool result = await academy.AddGroup(groupDto.Name, groupDto.Rating);
                            if(result)
                            {
                                var dataResponse = JsonSerializer.Serialize(new ResponseDTO() { Message="Added", 
                                IsSuccess=true});
                                byte[] bytes = Encoding.UTF8.GetBytes(dataResponse);
                                res.ContentType = "application/json; charset=utf-8";
                                res.ContentLength64 = bytes.Length;
                                res.StatusCode = 200;
                                using (Stream writer = res.OutputStream)
                                {

                                    await writer.WriteAsync(bytes, 0, bytes.Length);

                                }
                            }
                        }
                        

                    }
                       
                  
                }
                
                res.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private string GetPageName(string param) // /contacts
    {
        string result = param switch
        {
            "/contacts" => "contacts.html",
            "/about" => "about.html",
            "/" => "index.html",
            _ => "notfound.html"
        };
        return result;
    }
}
