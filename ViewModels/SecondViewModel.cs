using MauiStart.Base;

namespace MauiStart.ViewModels;

public class SecondViewModel : BaseViewModel
{

    public SecondViewModel()
    {
        
    }

    public override async Task OnViewAppearing()
    {
        await base.OnViewAppearing();
    }
}