using System;
using System.Net;
using System.Collections.Generic;
using System.Threading;


public class HttpResourceLocator
{
    private Dictionary<string, IHttpRequestHandler> handlerDictionary;

	public HttpResourceLocator()
	{
        handlerDictionary = new Dictionary<string, IHttpRequestHandler>();
	}

    public void AddRequestHandler(IHttpRequestHandler handler)
    {
        if (!handlerDictionary.ContainsKey(handler.GetName()))
        {
            handlerDictionary.Add(handler.GetName(), handler);   //currently, the handler doesn't exist in the dictionary, so add it
        }
        else
        {
            handlerDictionary[handler.GetName()] = handler;      //replace the request handler with the new one
        }
    }

    public void HandleContext(HttpListenerContext context)
    {
        Console.WriteLine("Request recieved");
        HttpListenerRequest request = context.Request;
        string requestHandlerName = request.Url.AbsolutePath;
        IHttpRequestHandler handler;
        if (handlerDictionary.ContainsKey(requestHandlerName))
        {
            handler = handlerDictionary[requestHandlerName];
        }
        else
        {
            handler = null;
            Console.WriteLine("invalid request");
        }
        this.InvokeHandler(handler, context);
    }

    private void InvokeHandler(IHttpRequestHandler handler, HttpListenerContext context)
    {
        if (handler == null)
            return;
        Console.WriteLine("Request being handled");
        HandleHttpRequestCommand command = new HandleHttpRequestCommand(handler, context);
        Thread commandThread = new Thread(command.Execute);
        commandThread.Start();
    }

    public class HandleHttpRequestCommand
    {
        private IHttpRequestHandler handler;
        private HttpListenerContext context;

        public HandleHttpRequestCommand(IHttpRequestHandler handler, HttpListenerContext context)
        {
            this.handler = handler;
            this.context = context;
        }

        public void Execute()
        {
            this.handler.Handle(this.context);
        }
    }



}
