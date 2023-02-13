using Microsoft.AspNetCore.Http;

namespace StadiumEngine.Handlers.Commands.Offers;

public class OffersImagePassed
{
    public string? Path { get; set; }
    public IFormFile? FormFile { get; set; }
    
}