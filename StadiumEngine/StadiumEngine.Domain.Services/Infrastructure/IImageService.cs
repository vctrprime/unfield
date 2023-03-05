using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StadiumEngine.Domain.Services.Infrastructure;

public interface IImageService
{
    Task<string> Upload( IFormFile file, int legalId, int stadiumId, string module );
    void Delete( string path );
}