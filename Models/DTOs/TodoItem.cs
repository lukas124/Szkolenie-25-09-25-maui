using SQLite;

namespace MauiStart.Models.DTOs;

[Table("TodoItems")]
public class TodoItem
{
    [PrimaryKey, AutoIncrement]
    public string ID { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public bool Done { get; set; }
}