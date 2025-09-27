using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Core.Extensions;
using MauiStart.Base;
using MauiStart.Models.Data;
using MauiStart.Models.Data.API.RequestProvider;
using MauiStart.Models.Domain.UseCases;
using MauiStart.Models.DTOs;
using NewRelic.MAUI.Plugin;

namespace MauiStart.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly IRequestProvider _requestProvider;
    private readonly CachePolicy _cachePolicy;
    
    public ICommand SelectItemCommand { get; }
    public ICommand CreateItemCommand { get; }
    public ICommand RemoveTodoItemCommand { get; }

    private string _selectedIndex;

    public string SelectedIndex
    {
        get => _selectedIndex;
        set => SetProperty(ref _selectedIndex, value);
    }

    private string _name;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string _notes;

    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }

    private bool _done;

    public bool Done
    {
        get => _done;
        set => SetProperty(ref _done, value);
    }

    private ObservableCollection<TodoItem> _items;
    public ObservableCollection<TodoItem> Items
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }

    public override bool CanNavigateBack => false;

    public MainViewModel(IRequestProvider requestProvider, CachePolicy cachePolicy)
    {
        _requestProvider = requestProvider;
        _cachePolicy = cachePolicy;
        
        SelectedIndex = "0";

        SelectItemCommand = new Command<string>(async (arg) => await SelectBox(arg));
        CreateItemCommand = new Command(async () => await CrateItemAsync());
        RemoveTodoItemCommand = new Command<TodoItem>(async (item) => await RemoveTdooItemAsync(item));
    }

    public override async Task OnViewAppearing()
    {
        await RefreshItemsAsync();
    }

    private async Task SelectBox(string boxNumber)
    {
        if (boxNumber == "0")
        {
            await RefreshItemsAsync();
        }
        SelectedIndex = boxNumber;
    }

    private async Task CrateItemAsync()
    {
        var newItem = new TodoItem
        {
            ID = Guid.NewGuid().ToString(),
            Name = Name,
            Notes = Notes,
            Done = Done
        };

        await new SendNewToDoItemUseCase(_requestProvider).ExecuteAsync(newItem);
        await SelectBox("0"); // go back to "Tab0"
    }

    private async Task<ObservableCollection<TodoItem>> GetTodoItemsAsync()
    {
        try
        {
            return (await new RetrieveToDoItemsUseCase(_requestProvider, _cachePolicy).ExecuteAsync()).ToObservableCollection();
        }
        catch (Exception e)
        {
            CrossNewRelic.Current.RecordException(e);
            return new ObservableCollection<TodoItem>();
        }
    }

    private async Task RemoveTdooItemAsync(TodoItem item)
    {
        await new RemoveToDoItemUseCase(_requestProvider).ExecuteAsync(item);
        await RefreshItemsAsync();
    }

    private async Task RefreshItemsAsync()
    {
        Items = await GetTodoItemsAsync();
    }
}