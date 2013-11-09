using System;
using System.Net;

/// <summary>
/// Interface for HttpRequestHandler
/// </summary>
public interface IHttpRequestHandler
{
    void Handle(HttpListenerContext context);

    string GetName();
}
