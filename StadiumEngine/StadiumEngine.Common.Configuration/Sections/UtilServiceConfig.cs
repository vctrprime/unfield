using Microsoft.Extensions.DependencyInjection;

namespace StadiumEngine.Common.Configuration.Sections;

public class UtilServiceConfig
{
    public string BaseUrl { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
}
