using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiStart.Base;
using MauiStart.Models;
using MauiStart.Models.Services.Interfaces;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace MauiStart.ViewModels;

public class MainViewModel : BaseViewModel
{
    public override bool CanNavigateBack => false;
    
    #region properties
    
    #endregion
    
    #region Commands 
    
    #endregion
    
    public MainViewModel()
    {
        // Dependencies
        
        
        // Commands
    }
    
    public override async Task OnViewAppearing()
    {
        await base.OnViewAppearing();
    }
}