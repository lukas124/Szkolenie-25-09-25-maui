using MauiStart.Models.Data.Database;
using MauiStart.Models.Data.Repositories;
using MauiStart.Models.DTOs;

namespace MauiStart.Models.Data.UoW;

public class RepositoriesUoW : IRepositoriesUoW
{
    private readonly AppDbContext _local;

    public RepositoriesUoW(AppDbContext local)
    {
        _local = local;
    }

    // Add new repositorie here
    public IRepository<TodoItem> TodoItems => 
        new TodoItemsRepository(_local);
}