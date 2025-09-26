namespace MauiStart.Models.Exceptions.Network;

public class ServiceAuthenticationEx : Exception
{
    public ServiceAuthenticationEx()
    {
    }

    public ServiceAuthenticationEx(string content)
    {
        Content = content;
    }

    public string Content { get; }
}