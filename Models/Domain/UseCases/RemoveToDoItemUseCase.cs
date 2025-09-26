using MauiStart.Models.Data.API.RequestProvider;

namespace MauiStart.Models.Domain.UseCases;

public class RemoveToDoItemUseCase : IUseCase<string, bool, object>
{
    private readonly IRequestProvider _requestProvider;
    
    public RemoveToDoItemUseCase(IRequestProvider requestProvider)
    {
        _requestProvider = requestProvider;
    }
    
    public async Task<bool> ExecuteAsync(string id, object callback = null)
    {
        try
        {
            await _requestProvider.DeleteAsync(string.Format("{0}/{1}", Constants.RestUrl, id), string.Empty);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }
}