using MauiStart.Models.Data.Database;
using MauiStart.Models.DTOs;

namespace MauiStart.Models.Data.Repositories;

public class TodoItemsRepository : IRepository<TodoItem>
{ 
    private readonly AppDbContext _local;

    public TodoItemsRepository(AppDbContext local)
    {
        _local = local;
    }
    
    public TodoItem New()
    {
        return new TodoItem();
    }

    public IQueryable<TodoItem> Query => _local.TodoItems;

    public async Task AddAsync(TodoItem entity)
    {
        await _local.TodoItems.AddAsync(entity);
        await _local.SaveChangesAsync();
    }
    
    public void Update(TodoItem entity)
    {
        _local.TodoItems.Update(entity);
        _local.SaveChanges();
    }
    
    public void Remove(TodoItem entity)
    {
        _local.TodoItems.Remove(entity);
        _local.SaveChanges();
    }
}