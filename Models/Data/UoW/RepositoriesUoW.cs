using MauiStart.Models.Data.Database;
using MauiStart.Models.Data.Repositories;
using MauiStart.Models.DTOs;

namespace MauiStart.Models.Data.UoW;

public class RepositoriesUoW : IRepositoriesUoW
{
    private readonly AppDbContext _dbContext;

    public RepositoriesUoW(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Repositories
    public IRepository<TodoItem> TodoItems =>
        new TodoItemsRepository(_dbContext);
}