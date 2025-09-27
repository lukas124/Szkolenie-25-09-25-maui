namespace MauiStart.Models.Services.Interfaces;

public interface IGooglePlayIntegrityService
{
    Task<string> GetIntegrityTokenAsync();
}