using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Services.Identity;

namespace StadiumEngine.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimsIdentityService, ClaimsIdentityService>();
        services.AddScoped<IPasswordGenerator, PasswordGenerator>();
    }
    
    
}