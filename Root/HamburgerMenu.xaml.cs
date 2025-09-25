namespace MauiStart.Root;

public partial class HamburgerMenu
{
    public HamburgerMenu()
    {
        InitializeComponent();
        BindingContext = new HamburgerMenuViewModel();
    }
}