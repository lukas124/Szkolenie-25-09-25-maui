using MauiStart.Models.Services.Implementations;
using MauiStart.Models.Services.Interfaces;
using MauiStart.ViewModels;
using CommunityToolkit.Maui;
using MauiStart.Models.Data;
using MauiStart.Models.Data.API.RequestProvider;
using MauiStart.Models.Data.Database;
using MauiStart.Models.Data.UoW;
#if ANDROID
using MauiStart.Platforms.Services;
#endif
using Microsoft.EntityFrameworkCore;

namespace MauiStart;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        RegisterServices(builder.Services);
        RegisterViewModels(builder.Services);
        ConfigureDb(builder.Services);
        
        var app = builder.Build();
        
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated(); // <-- Creates DB + tables if they don't exist
        }

        return app;
    }

    static void RegisterServices(in IServiceCollection services)
    {
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IRequestProvider>(
            sp =>
            {
#if RELEASE
                var authHttpHandler = new AuthHttpClientHandler();
                return new RequestProvider(authHttpHandler);
#endif
                var debugHttpHandler = sp.GetKeyedService<HttpMessageHandler>("DebugHttpMessageHandler");
                return new RequestProvider(debugHttpHandler);
                
            });
        services.AddSingleton<CachePolicy>();
        
        services.AddScoped<ICameraService, CameraService>();
        
#if ANDROID
        services.AddSingleton<IGooglePlayIntegrityService, GooglePlayIntegrityService>();
#endif
    }

    static void RegisterViewModels(in IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddTransient<SecondViewModel>();
    }
    
    static void ConfigureDb(in IServiceCollection services)
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Filename={dbPath}"));

        services.AddScoped<IRepositoriesUoW, RepositoriesUoW>();
    }
}