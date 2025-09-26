using MauiStart.Models.Data.Repositories;
using MauiStart.Models.DTOs;

namespace MauiStart.Models.Data.UoW;

public interface IRepositoriesUoW
{
    // Repositories
    IRepository<TodoItem> TodoItems { get; }
}