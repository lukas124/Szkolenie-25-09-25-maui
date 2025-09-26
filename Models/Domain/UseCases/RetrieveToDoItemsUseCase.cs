using MauiStart.Models.Data.API.RequestProvider;
using MauiStart.Models.DTOs;

namespace MauiStart.Models.Domain.UseCases;

public class RetrieveToDoItemsUseCase : IUseCase<IEnumerable<TodoItem>>
{
    private readonly IRequestProvider _requestProvider;
    
    public RetrieveToDoItemsUseCase(IRequestProvider requestProvider)
    {
        _requestProvider = requestProvider;
    }
    
    public async Task<IEnumerable<TodoItem>> ExecuteAsync()
    {
        var todoItems = await _requestProvider.GetAsync<List<TodoItem>>(Constants.RestUrl).ConfigureAwait(false);
        return todoItems ?? Enumerable.Empty<DTOs.TodoItem>();
    }
} 