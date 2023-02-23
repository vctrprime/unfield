using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.Services.Facades.Offers;
using StadiumEngine.Services.Identity;
using StadiumEngine.Services.Infrastructure;

namespace StadiumEngine.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimsIdentityService, ClaimsIdentityService>();
        services.AddScoped<IPasswordGenerator, PasswordGenerator>();
        services.AddScoped<IHasher, Hasher>();
        services.AddScoped<ISmsSender, SmsSender>();
        services.AddScoped<IPhoneNumberChecker, PhoneNumberChecker>();
        services.AddScoped<IPasswordValidator, PasswordValidator>();
        services.AddScoped<IImageService, ImageService>();

        #region facades

        services.AddScoped<IFieldFacade, FieldFacade>();
        
        #endregion
    }
    
    
}