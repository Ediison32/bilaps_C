namespace webInmobiliary.Domain.Interfaces;

public interface ICloudinaryRepository
{
    Task<string> UploadImageAsync(Stream filstream, string fileName);
}