using Microsoft.AspNetCore.Http;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Infrastructure;

internal class ImageService : IImageService
{
    private readonly string? _directory;

    public ImageService()
    {
        _directory = Environment.GetEnvironmentVariable( "IMAGE_STORAGE" );
    }


    public async Task<string> Upload( IFormFile file, int legalId, int stadiumId, string module )
    {
        var path = $"{legalId}/{stadiumId}/{module}";
        var fullPath = Path.Combine( _directory, path );
        var fileName = $"{Guid.NewGuid()}.jpg";

        Directory.CreateDirectory( fullPath );
        await using Stream stream = new FileStream( $"{fullPath}/{fileName}", FileMode.Create );
        await file.CopyToAsync( stream );

        return $"{path}/{fileName}";
    }

    public void Delete( string path )
    {
        var fullPath = Path.Combine( _directory, path );
        File.Delete( fullPath );
    }
}