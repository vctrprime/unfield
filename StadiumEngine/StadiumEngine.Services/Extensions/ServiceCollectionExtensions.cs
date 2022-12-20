using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Services.Auth.Abstract;
using StadiumEngine.Services.Auth.Concrete;

namespace StadiumEngine.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimsIdentityService, ClaimsIdentityService>();
    }
    
    
}