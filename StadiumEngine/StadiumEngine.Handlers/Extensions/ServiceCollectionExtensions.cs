using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Handlers.Facades;
using StadiumEngine.Handlers.Mappings;
using StadiumEngine.Services.Extensions;

namespace StadiumEngine.Handlers.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
        services.RegisterModules();
    }
    
    private static void RegisterModules(this IServiceCollection services)
    {
        services.RegisterServices();
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CommonProfile());
            cfg.AddProfile(new UtilsProfile());
            cfg.AddProfile(new AccountsProfile());
            cfg.AddProfile(new AdminProfile());
            cfg.AddProfile(new OffersProfile());
        }).CreateMapper());

        services.AddScoped<IAddUserHandlerFacade, AddUserHandlerFacade>();
    }
    
    
    
    
}