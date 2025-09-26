using System.Threading.Tasks;
using MauiStart.Models.Services;
using MauiStart.Models.Services.Implementations;
using MauiStart.Models.Services.Interfaces;
using MauiStart.Pages;
using MauiStart.Root;
using MauiStart.ViewModels;

namespace MauiStart;

public partial class App
{
    public App()
    {
        InitializeComponent();
        ConfigurationNavigations();

        MainPage = new RootPage();
        var navigationService = ServiceHelper.GetService<INavigationService>();

        // App is starting here...
        Task.Run(async () => { await navigationService.NavigateBackToHome(); }).Wait();
    }

    private static void ConfigurationNavigations()
    {
        NavigationService.ConfigureMap<MainViewModel, MainPage>();
        NavigationService.ConfigureMap<SecondViewModel, SecondPage>();
    }
}