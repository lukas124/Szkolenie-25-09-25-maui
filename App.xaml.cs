using MauiStart.Models.Services;
using MauiStart.Models.Services.Implementations;
using MauiStart.Models.Services.Interfaces;
using MauiStart.Pages;
using MauiStart.Root;
using MauiStart.ViewModels;
using NewRelic.MAUI.Plugin;

namespace MauiStart;

public partial class App
{
    public App()
    {
        InitializeComponent();
        ConfigurationNavigations();

        MainPage = new RootPage();
        
        CrossNewRelic.Current.HandleUncaughtException();
//      CrossNewRelic.Current.TrackShellNavigatedEvents();  // if the app uses shell

        // Set optional agent configuration
        // Options are: crashReportingEnabled, loggingEnabled, logLevel, collectorAddress, crashCollectorAddress
        AgentStartConfiguration agentConfig = new AgentStartConfiguration(true, true, LogLevel.INFO, "mobile-collector.newrelic.com", "mobile-crash.newrelic.com");

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            CrossNewRelic.Current.Start(Constants.NewRelicTokenAndroid);
            // Start with optional agent configuration 
            // CrossNewRelic.Current.Start("<YOUR_ANDROID_TOKEN>", agentConfig);
        } 
        else if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            CrossNewRelic.Current.Start(Constants.NewRelicTokeniOS);
            // Start with optional agent configuration 
            // CrossNewRelic.Current.Start("<YOUR_IOS_TOKEN>", agentConfig);
        }
        
        var navigationService = ServiceHelper.GetService<INavigationService>();

        // App is starting here...
        Task.Run(async () => { await navigationService!.NavigateBackToHome(); }).Wait();
    }

    private static void ConfigurationNavigations()
    {
        NavigationService.ConfigureMap<MainViewModel, MainPage>();
        NavigationService.ConfigureMap<SecondViewModel, SecondPage>();
    }
}