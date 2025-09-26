using MauiStart.Models.Data.API.RequestProvider;

namespace MauiStart.Models.Domain.UseCases;

public class RemoveToDoItemUseCase : IUseCase<string, bool, object>
{
    private readonly IRequestProvider _requestProvider;
    
    public RemoveToDoItemUseCase(IRequestProvider requestProvider)
    {
    }
}