using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MauiStart.Base.Interfaces;
using MauiStart.Models.Services;
using MauiStart.Models.Services.Interfaces;

namespace MauiStart.Base;

public abstract class BaseViewModel : INotifyPropertyChanged, INavigable
{
    private string _title = string.Empty;
    private bool _canNavigateBack = true;

    #region Properties

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public virtual bool CanNavigateBack
    {
        get => _canNavigateBack;
        set => SetProperty(ref _canNavigateBack, value);
    }

    #endregion

    public virtual Task OnNavigatedTo(object parameter)
    {
        return Task.FromResult(true);
    }
    
    public virtual async Task OnViewAppearing()
    {
    }

    public virtual async Task OnViewDisappearing()
    {
    }
    
    public virtual void OnBackButtonPressed()
    {
    }
    
    protected readonly INavigationService NavigationService;
    
    protected BaseViewModel()
    {
        NavigationService = ServiceHelper.GetService<INavigationService>();
    }
    
    #region INotifyPropertyChanged
    
    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        var changed = PropertyChanged;

        changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}