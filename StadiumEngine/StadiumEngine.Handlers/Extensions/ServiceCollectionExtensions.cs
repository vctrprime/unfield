using AutoMapper;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Q101.ServiceCollectionExtensions.ServiceCollectionExtensions;
using StadiumEngine.Handlers.Containers;
using StadiumEngine.Handlers.Containers.Utils;
using StadiumEngine.Handlers.Mappings;
using StadiumEngine.Repositories.Infrastructure.Extensions;
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
        services.RegisterDataAccessModules();
        services.RegisterServices();
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CommonProfile());
            cfg.AddProfile(new UtilsProfile());
            cfg.AddProfile(new AccountsProfile());
        }).CreateMapper());
        
        services.RegisterType<AddLegalHandlerRepositoriesContainer>().AsScoped().PropertiesAutowired().Bind();
        services.RegisterType<AddLegalHandlerServicesContainer>().AsScoped().PropertiesAutowired().Bind();
        services.RegisterType<SyncPermissionsHandlerRepositoriesContainer>().AsScoped().PropertiesAutowired().Bind();
    }
    
    
    
    
}