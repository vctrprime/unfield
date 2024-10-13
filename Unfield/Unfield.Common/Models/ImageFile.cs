using Microsoft.AspNetCore.Http;

namespace Unfield.Common.Models;

public class ImageFile
{
    public string? Path { get; set; }
    public IFormFile? FormFile { get; set; }
}