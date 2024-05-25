using System.Net;

namespace NASSG;
public class SimpleWebServer
{
    private readonly HttpListener _listener = new HttpListener();
    private readonly string _baseDirectory;

    public SimpleWebServer(string prefix, string baseDirectory)
    {
        _listener.Prefixes.Add(prefix);
        _baseDirectory = baseDirectory;
    }

    public void Start()
    {
        _listener.Start();
        Console.WriteLine($"Serving files from {_baseDirectory} on {_listener.Prefixes.First()}");
        Task.Run(() => Listen());
    }

    private async Task Listen()
    {
        while (_listener.IsListening)
        {
            var context = await _listener.GetContextAsync();
            await Task.Run(() => ProcessRequest(context));
        }
    }

    private void ProcessRequest(HttpListenerContext context)
    {
        string urlPath = context.Request.Url?.AbsolutePath.Trim('/') ?? string.Empty;
        string filePath = Path.Combine(_baseDirectory, urlPath);

        if (Directory.Exists(filePath))
        {
            filePath = Path.Combine(filePath, "index.html");
        }

        if (File.Exists(filePath))
        {
            byte[] content = File.ReadAllBytes(filePath);
            context.Response.ContentType = GetContentType(filePath);
            context.Response.ContentLength64 = content.Length;
            context.Response.OutputStream.Write(content, 0, content.Length);
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            using (var writer = new StreamWriter(context.Response.OutputStream))
            {
                writer.Write("404 - File Not Found");
            }
        }

        context.Response.OutputStream.Close();
    }

    private string GetContentType(string filePath)
    {
        return Path.GetExtension(filePath).ToLower() switch
        {
            ".html" => "text/html",
            ".css" => "text/css",
            ".js" => "application/javascript",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".svg" => "image/svg+xml",
            _ => "application/octet-stream",
        };
    }

    public void Stop()
    {
        _listener.Stop();
        _listener.Close();
    }
}
