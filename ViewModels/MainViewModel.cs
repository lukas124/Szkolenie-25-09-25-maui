using System.Collections;
using System.Windows.Input;
using MauiStart.Base;
using MauiStart.Models.Data.API.RequestProvider;
using MauiStart.Models.Data.Repositories;
using MauiStart.Models.Data.UoW;
using MauiStart.Models.Domain.UseCases;
using MauiStart.Models.DTOs;

namespace MauiStart.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly IRequestProvider _requestProvider;
    private readonly IRepositoriesUoW _repositoriesUoW;
    
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

    public MainViewModel(IRequestProvider requestProvider, IRepositoriesUoW  repositoriesUoW)
    {
        _requestProvider = requestProvider;
        _repositoriesUoW = repositoriesUoW;
        
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
    }

    private async Task<IList<TodoItem>> GetTodoItemsAsync(int page, int pageSize)
    {
        return (await new RetrieveToDoItemsUseCase(_requestProvider, _repositoriesUoW).ExecuteAsync()).ToList();
    }

    private async Task RemoveTdooItemAsync(string id)
    {
    }
}