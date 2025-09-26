namespace MauiStart.Models.Domain.UseCases;

public interface IUseCase<Input, Output, Callback>
{
    Task<Output> ExecuteAsync(Input input, Callback callback = default(Callback));
}

public interface IUseCase<Input, Callback>
{
    Task ExecuteAsync(Input input, Callback callback = default(Callback));
}

public interface IUseCase<Output>
{
    Task<Output> ExecuteAsync();
}

public interface IUseCase
{
    Task ExecuteAsync();
}