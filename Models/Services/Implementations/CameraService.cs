using MauiStart.Models.Services.Interfaces;

namespace MauiStart.Models.Services.Implementations;

public class CameraService : ICameraService
{
    public async Task<byte[]?> TakePhotoAsync()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo == null)
                return null;
            
            byte[] imageData;
            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using Stream stream = await photo.OpenReadAsync();
            using (FileStream localFileStream = File.OpenWrite(localFilePath))
            {
                await stream.CopyToAsync(localFileStream);
            }
            return File.ReadAllBytes(localFilePath);
        }

        return null;
    }
}