using MauiStart.Models.Data.API.RequestProvider;
using MauiStart.Models.DTOs;

namespace MauiStart.Models.Domain.UseCases;

public class SendNewToDoItemUseCase : IUseCase<TodoItem, object> 
{
    private readonly IRequestProvider _requestProvider;
    
    public SendNewToDoItemUseCase(IRequestProvider requestProvider)
    {
        _requestProvider = requestProvider;
    }
    
    public Task ExecuteAsync(TodoItem input, object callback = null)
    {
        return _requestProvider.PostAsync(Constants.RestUrl, input);
    }
}