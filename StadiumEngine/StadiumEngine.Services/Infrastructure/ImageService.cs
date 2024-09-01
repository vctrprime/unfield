using Microsoft.AspNetCore.Http;
using StadiumEngine.Common.Configuration.Sections;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Infrastructure;

internal class ImageService : IImageService
{
    private readonly string? _directory;

    public ImageService( StorageConfig storageConfig )
    {
        _directory = storageConfig.ImageStorage;
    }


    public async Task<string> Upload( IFormFile file, int stadiumGroupId, int stadiumId, string module )
    {
        if ( _directory is null )
        {
            return String.Empty;
        }

        string path = $"{stadiumGroupId}/{stadiumId}/{module}";
        string fullPath = Path.Combine( _directory, path );
        string fileName = $"{Guid.NewGuid()}.jpg";

        Directory.CreateDirectory( fullPath );
        await using Stream stream = new FileStream( $"{fullPath}/{fileName}", FileMode.Create );
        await file.CopyToAsync( stream );

        return $"{path}/{fileName}";
    }

    public void Delete( string path )
    {
        if ( _directory is null )
        {
            return;
        }

        string fullPath = Path.Combine( _directory, path );
        File.Delete( fullPath );
    }
}