namespace MauiStart.Models.Services.Interfaces;

public interface ICameraService
{
    Task<byte[]?> TakePhotoAsync();
}