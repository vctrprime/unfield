using AutoMapper;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Handlers.Mappings;
using StadiumEngine.Repositories.Extensions;

namespace StadiumEngine.Handlers.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.RegisterDataAccessModules();
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new TestProfile());
        }).CreateMapper());

        
    }
    
    
}