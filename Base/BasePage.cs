using System.Windows.Input;
using MauiStart.Base.Interfaces;
using MauiStart.Root;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Graphics;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace MauiStart.Base;

public abstract class BasePage : ContentPage
{
    public static readonly BindableProperty CanNavigateBackProperty = BindableProperty.Create(
        nameof(CanNavigateBack),
        typeof(bool),
        typeof(BasePage),
        false);

    protected BasePage()
    {
        InitializeView();

        SetValue(NavigationPage.HasNavigationBarProperty, false);
        SetBinding(IsBusyProperty, new Binding(nameof(IsBusy)));

        On<iOS>().SetUseSafeArea(true);

        MenuButtonCommand = new Command(MenuButtonAction);
        BackButtonCommand = new Command(BackButtonAction);

        ControlTemplate = new AppHeader();
    }

    public ICommand MenuButtonCommand { get; }
    public ICommand BackButtonCommand { get; }
    
    public bool CanNavigateBack
    {
        get => (bool)GetValue(CanNavigateBackProperty);
        set => SetValue(CanNavigateBackProperty, value);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (BindingContext is INavigable navigable)
        {
            CanNavigateBack = navigable.CanNavigateBack;
        }
    }

    private static void MenuButtonAction(object obj)
    {
        if (Application.Current.MainPage is RootPage rootPage)
        {
            rootPage.ShowHamburgerMenu = !rootPage.ShowHamburgerMenu;
        }
    }

    private static void BackButtonAction()
    {
        if (Application.Current.MainPage is RootPage rootPage)
        {
            rootPage.SendBackButtonPressed();
        }
    }

    private void InitializeView()
    {
        BackgroundColor = Colors.White;
    }

    protected override void OnAppearing()
    {
        if (BindingContext is BaseViewModel baseViewModel)
        {
            baseViewModel.OnViewAppearing();
        }

        base.OnAppearing();
    }
    
    protected override void OnDisappearing()
    {
        if (BindingContext is BaseViewModel baseViewModel)
        {
            baseViewModel.OnViewDisappearing();
        }

        base.OnDisappearing();
    }

    protected override bool OnBackButtonPressed()
    {
        if (BindingContext is BaseViewModel baseViewModel)
        {
            if (!baseViewModel.CanNavigateBack) return false;
            baseViewModel.OnBackButtonPressed();
        }

        return base.OnBackButtonPressed();
    }
}