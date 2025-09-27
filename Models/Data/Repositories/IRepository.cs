namespace MauiStart.Models.Data.Repositories;

public interface IRepository<T> where T : class
{
    T New();
    IQueryable<T> Query { get; }
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}