namespace MauiStart;

public static class Constants
{
    public static string LocalhostUrl = DeviceInfo.Platform == DevicePlatform.Android ? "10.0.2.2" : "localhost";
    public static string Scheme = "http"; // or https
    public static string Port = "5000";
    public static string RestUrl = $"{Scheme}://{LocalhostUrl}:{Port}/api/todoitems";
}
