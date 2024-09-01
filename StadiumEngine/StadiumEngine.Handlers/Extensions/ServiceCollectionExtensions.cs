using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Common.Configuration;
using StadiumEngine.Common.Configuration.Sections;
using StadiumEngine.Handlers.Builders.BookingForm;
using StadiumEngine.Handlers.Facades.Accounts.Legals;
using StadiumEngine.Handlers.Facades.Accounts.Users;
using StadiumEngine.Handlers.Facades.Offers.Fields;
using StadiumEngine.Handlers.Facades.Offers.Inventories;
using StadiumEngine.Handlers.Facades.Offers.LockerRooms;
using StadiumEngine.Handlers.Facades.Rates.PriceGroups;
using StadiumEngine.Handlers.Facades.Rates.Prices;
using StadiumEngine.Handlers.Facades.Rates.Tariffs;
using StadiumEngine.Handlers.Facades.Settings.Breaks;
using StadiumEngine.Handlers.Facades.Settings.Main;
using StadiumEngine.Handlers.Mappings;
using StadiumEngine.Handlers.Processors.BookingForm;
using StadiumEngine.Handlers.Resolvers.Customers;
using StadiumEngine.Jobs.Infrastructure.Extensions;
using StadiumEngine.Services.Extensions;

namespace StadiumEngine.Handlers.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterHandlers( 
        this IServiceCollection services, 
        ConnectionsConfig connectionsConfig ) => 
        services.RegisterModules( connectionsConfig );

    private static void RegisterModules( 
        this IServiceCollection services, 
        ConnectionsConfig connectionsConfig )
    {
        services.RegisterServices( connectionsConfig );
        services.AddMediator( options => options.ServiceLifetime = ServiceLifetime.Scoped );
        
        services.AddScoped<IAddUserFacade, AddUserFacade>();
        services.AddScoped<IUpdateUserFacade, UpdateUserFacade>();
        services.AddScoped<IChangeLegalFacade, ChangeLegalFacade>();

        services.AddScoped<IUpdateFieldFacade, UpdateFieldFacade>();
        services.AddScoped<IUpdateInventoryFacade, UpdateInventoryFacade>();
        services.AddScoped<IUpdateLockerRoomFacade, UpdateLockerRoomFacade>();

        services.AddScoped<IUpdatePriceGroupFacade, UpdatePriceGroupFacade>();
        services.AddScoped<IUpdateTariffFacade, UpdateTariffFacade>();

        services.AddScoped<ISetPricesFacade, SetPricesFacade>();

        services.AddScoped<IUpdateMainSettingsFacade, UpdateMainSettingsFacade>();
        services.AddScoped<IUpdateBreakFacade, UpdateBreakFacade>();
        
        services.AddScoped<IBookingFormDtoBuilder, BookingFormDtoBuilder>();
        services.AddScoped<IBookingCheckoutDtoBuilder, BookingCheckoutDtoBuilder>();
        
        services.AddScoped<IBookingAuthorizedCustomerResolver, BookingAuthorizedCustomerResolver>();
        services.AddScoped<IMovingBookingFormProcessor, MovingBookingFormProcessor>();
        
        MapperConfiguration mappingConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile( new CommonProfile() );
                cfg.AddProfile( new UtilsProfile() );
                cfg.AddProfile( new AccountsProfile() );
                cfg.AddProfile( new AdminProfile() );
                cfg.AddProfile( new OffersProfile() );
                cfg.AddProfile( new RatesProfile() );
                cfg.AddProfile( new SettingsProfile() );
                cfg.AddProfile( new GeoProfile() );
                cfg.AddProfile( new BookingFormProfile() );
                cfg.AddProfile( new SchedulerProfile() );
                cfg.AddProfile( new NotificationsProfile() );
                cfg.AddProfile( new DashboardsProfile() );
                cfg.AddProfile( new CustomersProfile() );
            } );
        services.AddSingleton( provider => mappingConfig.CreateMapper());
        services.RegisterJobs( connectionsConfig.MainDb );
    }
}