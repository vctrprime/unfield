using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Handlers.Facades.Accounts.Legals;
using StadiumEngine.Handlers.Facades.Accounts.Users;
using StadiumEngine.Handlers.Facades.Offers.Fields;
using StadiumEngine.Handlers.Facades.Offers.Inventories;
using StadiumEngine.Handlers.Facades.Offers.LockerRooms;
using StadiumEngine.Handlers.Facades.Rates.PriceGroups;
using StadiumEngine.Handlers.Facades.Rates.Tariffs;
using StadiumEngine.Handlers.Mappings;
using StadiumEngine.Services.Extensions;

namespace StadiumEngine.Handlers.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterHandlers( this IServiceCollection services )
    {
        services.AddMediator( options => options.ServiceLifetime = ServiceLifetime.Scoped );
        services.RegisterModules();
    }

    private static void RegisterModules( this IServiceCollection services )
    {
        services.RegisterServices();
        services.AddMediator( options => options.ServiceLifetime = ServiceLifetime.Scoped );
        services.AddSingleton(
            provider => new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile( new CommonProfile() );
                    cfg.AddProfile( new UtilsProfile() );
                    cfg.AddProfile( new AccountsProfile() );
                    cfg.AddProfile( new AdminProfile() );
                    cfg.AddProfile( new OffersProfile() );
                    cfg.AddProfile( new RatesProfile() );
                } ).CreateMapper() );

        services.AddScoped<IAddUserFacade, AddUserFacade>();
        services.AddScoped<IUpdateUserFacade, UpdateUserFacade>();
        services.AddScoped<IChangeLegalFacade, ChangeLegalFacade>();

        services.AddScoped<IUpdateFieldFacade, UpdateFieldFacade>();
        services.AddScoped<IUpdateInventoryFacade, UpdateInventoryFacade>();
        services.AddScoped<IUpdateLockerRoomFacade, UpdateLockerRoomFacade>();

        services.AddScoped<IUpdatePriceGroupFacade, UpdatePriceGroupFacade>();
        services.AddScoped<IUpdateTariffFacade, UpdateTariffFacade>();
    }
}