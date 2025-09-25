using MauiStart.Base;

namespace MauiStart.Models.Services;

public static class ServiceHelper
{
    public static TService? GetService<TService>()
        => Current.GetService<TService>();

    public static BaseViewModel? GetViewModel(Type type)
        => Current.GetService(type) as BaseViewModel;

    public static IServiceProvider Current
        =>
#if ANDROID
            MauiApplication.Current.Services;
#elif IOS
            MauiUIApplicationDelegate.Current.Services;
#endif
}