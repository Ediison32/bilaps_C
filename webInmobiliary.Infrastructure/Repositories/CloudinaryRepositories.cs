using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using webInmobiliary.Domain.Interfaces;

namespace webInmobiliary.Infrastructure.Repositories;

public class CloudinaryRepositories : ICloudinaryRepository
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryRepositories(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }
    
    public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, fileStream),
            Folder = "inmobiliary/properties"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.Error != null)
            throw new Exception($"Cloudinary Error: {result.Error.Message}");

        return result.SecureUrl.AbsoluteUri;
    }
}