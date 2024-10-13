using Microsoft.AspNetCore.Http;

namespace Unfield.Domain.Services.Infrastructure;

public interface IImageService
{
    Task<string> Upload( IFormFile file, int stadiumGroupId, int stadiumId, string module );
    void Delete( string path );
}