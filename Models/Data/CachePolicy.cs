namespace MauiStart.Models.Data;

public class CachePolicy
{
    public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(10);
    public DateTime LastUpdated { get; set; } = DateTime.MinValue;

    public bool IsExpired() => DateTime.UtcNow - LastUpdated > Expiration;
}