using System.Net;

namespace MauiStart.Models.Exceptions.Network;

public class HttpRequestEx : HttpRequestException
{
    public HttpRequestEx(HttpStatusCode code) : this(code, null, null)
    {
    }

    public HttpRequestEx(HttpStatusCode code, string message) : this(code, message, null)
    {
    }

    public HttpRequestEx(HttpStatusCode code, string message, Exception inner) : base(message, inner)
    {
        HttpCode = code;
    }

    public HttpStatusCode HttpCode { get; }
}