using System.Collections;
using System.Windows.Input;
using MauiStart.Base;
using MauiStart.Models.Data.API.RequestProvider;
using MauiStart.Models.Domain.UseCases;
using MauiStart.Models.DTOs;

namespace MauiStart.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly IRequestProvider _requestProvider;
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

    public override bool CanNavigateBack => false;

    public Func<int, int, Task<IList>> TodoItemsProvider =>
        async (page, pageSize) => (IList)await GetTodoItemsAsync(page, pageSize);

    public MainViewModel(IRequestProvider requestProvider)
    {
        _requestProvider = requestProvider;
        SelectedIndex = "0";

        SelectItemCommand = new Command<string>((arg) => SelectBox(arg));
        CreateItemCommand = new Command(async () => await CrateItemAsync());
        RemoveTodoItemCommand = new Command<string>(async (id) => await RemoveTdooItemAsync(id));
    }

    public override async Task OnViewAppearing()
    {
        await base.OnViewAppearing();
    }

    private void SelectBox(string boxNumber)
    {
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
    }

    private async Task<IList<TodoItem>> GetTodoItemsAsync(int page, int pageSize)
    {
        return (await new RetrieveToDoItemsUseCase(_requestProvider).ExecuteAsync()).ToList();
    }

    private async Task RemoveTdooItemAsync(string id)
    {
        await new RemoveToDoItemUseCase(_requestProvider).ExecuteAsync(id);
        await RefreshItemsAsync();
    }

    private async Task RefreshItemsAsync()
    {
        await NavigationService.NavigateToAsync<MainViewModel>();
    }
}