using System.Text.Json.Serialization;
using MauiStart.Models.DTOs;

namespace MauiStart.Models.Data.API;

[JsonSourceGenerationOptions(
    PropertyNameCaseInsensitive = true,
    NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(List<TodoItem>))]
internal partial class MauiStartJsonSerializerContext : JsonSerializerContext
{
}