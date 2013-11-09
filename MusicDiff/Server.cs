using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Server : IDisposable
{
    private HttpListener listener = null;
    private Thread thread = null;
    private Boolean isRunning, isDisposed;

    private HttpResourceLocator resourceLocator = null;

    public Server(string prefix)
    {
        if (!HttpListener.IsSupported)
        {
            Console.WriteLine("server is not supported");
        }
        listener = new HttpListener();
        listener.Prefixes.Add(prefix);
        resourceLocator = new HttpResourceLocator();
    }

    public void AddRequestHandler(IHttpRequestHandler handler)
    {
        resourceLocator.AddRequestHandler(handler);
    }

    public void Start()
    {
        if (!listener.IsListening)
        {
            listener.Start();
            isRunning = true;
            thread = new Thread(new ThreadStart(this.ConnectionThreadStart));
            thread.Start();
            Console.WriteLine("Server has started");
        }
    }

    public void Stop()
    {
        if (listener.IsListening)
        {
            isRunning = false;
            listener.Stop();
        }
    }

    private void ConnectionThreadStart()
    {
        try
        {
            while (isRunning)
            {
                HttpListenerContext context = listener.GetContext();
                resourceLocator.HandleContext(context);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Exception thrown from the server");
        }
    }

    public virtual void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (isDisposed)
        {
            return;
        }
        if (disposing)
        {
            if (isRunning)
            {
                Stop();
            }
            if (thread != null)
            {
                thread.Abort();
                thread = null;
            }
        }
        isDisposed = true;
    }

}
