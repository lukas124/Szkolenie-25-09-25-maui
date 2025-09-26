using MauiStart.Models.Data.Database;
using MauiStart.Models.DTOs;

namespace MauiStart.Models.Data.Repositories;

public class TodoItemsRepository : IRepository<TodoItem>
{
    private readonly AppDbContext _dbContext;

    public TodoItemsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public TodoItem New()
    {
        return new TodoItem();
    }

    public IQueryable<TodoItem> Query => _dbContext.TodoItems;
    
    public async Task AddAsync(TodoItem entity)
    {
        await _dbContext.TodoItems.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<TodoItem> entities)
    {
        await _dbContext.TodoItems.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }

    public void Update(TodoItem entity)
    {
        _dbContext.TodoItems.Update(entity);
        _dbContext.SaveChanges();
    }

    public void Remove(TodoItem entity)
    {
        _dbContext.TodoItems.Remove(entity);
        _dbContext.SaveChanges();
    }
}