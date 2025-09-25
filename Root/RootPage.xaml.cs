namespace MauiStart.Root;

public partial class RootPage
{
    public new INavigation Navigation => Container.Navigation;

    public RootPage()
    {
        InitializeComponent();
    }
}