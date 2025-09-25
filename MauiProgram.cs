using MauiStart.Models.Services.Implementations;
using MauiStart.Models.Services.Interfaces;
using MauiStart.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace MauiStart;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        RegisterServices(builder.Services);
        RegisterViewModels(builder.Services);

        return builder.Build();
    }

    static void RegisterServices(in IServiceCollection services)
    {
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddScoped<ICameraService, CameraService>();
    }

    static void RegisterViewModels(in IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddTransient<SecondViewModel>();
    }
}