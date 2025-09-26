using MauiStart.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MauiStart.Models.Data.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems { get; set; }
}