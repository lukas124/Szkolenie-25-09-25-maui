using MauiStart.Models.Data.API.RequestProvider;
using MauiStart.Models.Data.UoW;
using MauiStart.Models.DTOs;

namespace MauiStart.Models.Domain.UseCases;

public class RetrieveToDoItemsUseCase : IUseCase<IEnumerable<TodoItem>>
{
    private readonly IRequestProvider _requestProvider;
    private readonly IRepositoriesUoW _repositoriesUoW;
    
    public RetrieveToDoItemsUseCase(IRequestProvider requestProvider, IRepositoriesUoW repositoryUoW)
    {
        _requestProvider = requestProvider;
        _repositoriesUoW = repositoryUoW;
    }
    
    public async Task<IEnumerable<TodoItem>> ExecuteAsync()
    {
        var todoItems = await _requestProvider.GetAsync<List<TodoItem>>(Constants.RestUrl).ConfigureAwait(false);

        foreach (var todoItem in todoItems)
        {
            _repositoriesUoW.TodoItems.Update(todoItem);
        }
        
        //await _repositoriesUoW.TodoItems.AddRangeAsync(todoItems);

        var test = _repositoriesUoW.TodoItems.Query.Where(x => x.Done).ToList();
        return test;
    }
} 