using System;
using System.Net;
using System.Text;

public class DiffSyncRequestHandler : IHttpRequestHandler
{
    public void Handle(HttpListenerContext context)
    {
        HttpListenerResponse resp = context.Response;
        string message = "you just sent me a message";
        byte[] bytes = Encoding.Default.GetBytes(message);
        resp.OutputStream.Write(bytes, 0, bytes.Length);
        resp.Close();
        Console.WriteLine("handled diff sync request");
    }

    public string GetName()
    {
        return "/sync";
    }

}
