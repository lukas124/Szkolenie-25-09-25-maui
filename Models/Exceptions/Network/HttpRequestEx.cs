using System.Net;

namespace MauiStart.Models.Exceptions.Network;

public class HttpRequestExceptionException : HttpRequestException
{
    public HttpRequestExceptionException(HttpStatusCode code) : this(code, null, null)
    {
    }

    public HttpRequestExceptionException(HttpStatusCode code, string message) : this(code, message, null)
    {
    }

    public HttpRequestExceptionException(HttpStatusCode code, string message, Exception inner) : base(message, inner)
    {
        HttpCode = code;
    }

    public HttpStatusCode HttpCode { get; }
}