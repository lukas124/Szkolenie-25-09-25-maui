using MauiStart.Base;
using MauiStart.Models.Services.Interfaces;
using MauiStart.Root;
using MauiStart.ViewModels;

namespace MauiStart.Models.Services.Implementations;

public class NavigationService : INavigationService
{
    private static readonly Dictionary<Type, Type> Mappings = new Dictionary<Type, Type>();
    private bool _isInitialized;
    private readonly Application? _application = Application.Current;
    
    public async Task NavigateBackToHome()
    {
        if (_application?.MainPage is RootPage appPage)
        {
            await ClearModalStack(appPage);
            if (!_isInitialized)
            {
                await InitializeRootPage(appPage);
            }

            await appPage.Navigation.PopToRootAsync(false);
            appPage.ShowHamburgerMenu = false;
        }
        else
        {
            throw new ArgumentNullException($"Missing {nameof(RootPage)}");
        }
    }

    private async Task ClearModalStack(RootPage rootPage)
    {
        if (rootPage.Navigation.ModalStack.Any())
        {
            await rootPage.Navigation.PopToRootAsync();
        }            
    }

    public async Task NavigateToModal<TViewModel>(bool animated = false) where TViewModel : BaseViewModel
    {
        await NavigateToAsync(typeof(TViewModel), null, animated, true);
    }

    public async Task NavigateToAsync<TViewModel>(object parameter = null, bool animated = true) where TViewModel : BaseViewModel
    {
        await NavigateToAsync(typeof(TViewModel), parameter, animated);
    }

    private async Task NavigateToAsync(Type type, object parameter, bool animated = true, bool showModal = false)
    {
        var view = GenerateView(type);
        var viewModel = GenerateViewModel(type);

        view.BindingContext = viewModel;

        if (_application?.MainPage is RootPage rootPage)
        {
            if (!_isInitialized)
            {
                await InitializeRootPage(rootPage);
            }
            
            rootPage.ShowHamburgerMenu = false;
            await viewModel.OnNavigatedTo(parameter);
            
            if (showModal)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                    await rootPage.Navigation.PushModalAsync(view, animated));
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                    await rootPage.Navigation.PushAsync(view, animated));
            }
        }
        
        Navigated?.Invoke(this, EventArgs.Empty);
    }

    public async Task NavigateBackAsync(bool isModal = false)
    {
        if (_application?.MainPage is RootPage currentPage)
        {
            if (isModal)
            {
                await currentPage.Navigation.PopModalAsync();
            }
            else
            {
                await currentPage.Navigation.PopAsync(true);
            }
        }
    }
    
    private async Task InitializeRootPage(RootPage appPage)
    {
        var view = GenerateView(typeof(MainViewModel));
        var viewModel = GenerateViewModel(typeof(MainViewModel));
        
        await viewModel.OnNavigatedTo(null);

        view.BindingContext = viewModel;
        await appPage.Navigation.PushAsync(view);
        _isInitialized = true;
    }

    private BaseViewModel GenerateViewModel(Type type)
    {
        return ServiceHelper.GetViewModel(type);
    }

    private static Page GenerateView(Type type)
    {
        var pageType = GetViewType(type);

        if (!(Activator.CreateInstance(pageType) is Page page))
        {
            throw new Exception($"Missing {nameof(type)}");
        }

        return page;
    }

    private static Type GetViewType(Type type)
    {
        if (!Mappings.TryGetValue(type, out var viewType))
            throw new KeyNotFoundException();

        return viewType;
    }

    public event EventHandler? Navigated;

    public static void ConfigureMap<TViewModel, TPage>() where TViewModel : BaseViewModel where TPage : Page
    {
        if (!Mappings.ContainsKey(typeof(TViewModel)))
        {
            Mappings.Add(typeof(TViewModel), typeof(TPage));
        }
    }
}