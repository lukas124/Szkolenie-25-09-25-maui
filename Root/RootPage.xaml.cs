namespace MauiStart.Root;

public partial class RootPage
{
    private bool _showHamburgerMenu;

    public static readonly BindableProperty ShowHamburgerMenuProperty = BindableProperty.Create(
        nameof(ShowHamburgerMenu),
        typeof(bool),
        typeof(RootPage),
        false);
    
    public new INavigation Navigation => Container.Navigation;
    
    public bool ShowHamburgerMenu
    {
        get { return (bool)GetValue(ShowHamburgerMenuProperty); }
        set { SetValue(ShowHamburgerMenuProperty, value); }
    }

    public RootPage()
    {
        InitializeComponent();
    }
}