using AutoMapper;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Handlers.Mappings;
using StadiumEngine.Repositories.Infrastructure.Extensions;
using StadiumEngine.Services.Extensions;

namespace StadiumEngine.Handlers.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.RegisterDataAccessModules();
        services.RegisterServices();
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AccountsProfile());
        }).CreateMapper());

        
    }
    
    
}