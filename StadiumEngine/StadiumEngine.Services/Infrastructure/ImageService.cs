using Microsoft.AspNetCore.Http;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Infrastructure;

internal class ImageService : IImageService
{
    private readonly string? _directory;

    public ImageService()
    {
        _directory = Environment.GetEnvironmentVariable("IMAGE_STORAGE");
    }


    public async Task<string> Upload(IFormFile file, int legalId, int stadiumId, string module)
    {
        var path = $"{legalId}/{stadiumId}/{module}/{Guid.NewGuid()}.jpg";
        var fullPath = Path.Combine(_directory, path);

        await using Stream stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        return path;
    }

    public void Delete(string path)
    {
        var fullPath = Path.Combine(_directory, path);
        File.Delete(fullPath);
    }
    
}