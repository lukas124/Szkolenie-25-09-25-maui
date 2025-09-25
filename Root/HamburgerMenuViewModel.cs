using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiStart.Base;
using MauiStart.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiStart.Root;

public class HamburgerMenuViewModel : BaseViewModel
{
    private ObservableCollection<MenuItem> _menuItems;
    public ObservableCollection<MenuItem> MenuItems
    {
        get => _menuItems;
        set => SetProperty(ref _menuItems, value);
    }
    
    public HamburgerMenuViewModel()
    {
        MenuItems = new ObservableCollection<MenuItem>(
            new[]
            {
                new MenuItem
                {
                    Title = "Main page",
                    Command = new Command(async () =>
                        await NavigationService.NavigateBackToHome()),
                    Icon = ImageSource.FromFile("play.png")
                },
            });
    }
}

// Model
public class MenuItem
{
    public string Title { get; set; } 
    public ImageSource Icon { get; set; }
    public ICommand Command { get; set; }
}