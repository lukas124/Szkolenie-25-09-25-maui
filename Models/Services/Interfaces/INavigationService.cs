using MauiStart.Base;

namespace MauiStart.Models.Services.Interfaces;

public interface INavigationService
{ 
    Task NavigateBackToHome();
    Task NavigateToModal<TViewModel>(bool animated = false) where TViewModel : BaseViewModel;
    Task NavigateToAsync<TViewModel>(object parameter = null, bool animated = true) where TViewModel : BaseViewModel;
    Task NavigateBackAsync(bool isModal = false);
    event EventHandler Navigated;
}