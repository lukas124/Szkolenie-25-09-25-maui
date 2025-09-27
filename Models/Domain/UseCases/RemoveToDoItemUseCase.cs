using MauiStart.Models.Data.API.RequestProvider;
using MauiStart.Models.Data.UoW;
using MauiStart.Models.DTOs;
using MauiStart.Models.Services;
using NewRelic.MAUI.Plugin;

namespace MauiStart.Models.Domain.UseCases;

public class RemoveToDoItemUseCase : IUseCase<TodoItem, bool, object>
{
    private readonly IRequestProvider _requestProvider;
    
    public RemoveToDoItemUseCase(IRequestProvider requestProvider)
    {
        _requestProvider = requestProvider;
    }
    
    public async Task<bool> ExecuteAsync(TodoItem todoItem, object callback = null)
    {
        // if success or not
        try
        {
            await _requestProvider.DeleteAsync(string.Format("{0}/{1}", Constants.RestUrl, todoItem.ID), string.Empty);
        }
        catch (Exception ex)
        {
            return false;
        }
        
        // remove the same element in db
        var repositoriesUoW = ServiceHelper.GetService<IRepositoriesUoW>();
        try
        {
            repositoriesUoW!.TodoItems.Remove(todoItem);
        }
        catch (Exception e)
        {
            CrossNewRelic.Current.RecordException(e);
        }

        return true;
    }
}