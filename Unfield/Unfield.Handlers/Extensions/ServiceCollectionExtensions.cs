using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Unfield.Common.Configuration;
using Unfield.Common.Configuration.Sections;
using Unfield.Handlers.Builders.BookingForm;
using Unfield.Handlers.Facades.Accounts.StadiumGroups;
using Unfield.Handlers.Facades.Accounts.Users;
using Unfield.Handlers.Facades.Bookings;
using Unfield.Handlers.Facades.Customers;
using Unfield.Handlers.Facades.Offers.Fields;
using Unfield.Handlers.Facades.Offers.Inventories;
using Unfield.Handlers.Facades.Offers.LockerRooms;
using Unfield.Handlers.Facades.Rates.PriceGroups;
using Unfield.Handlers.Facades.Rates.Prices;
using Unfield.Handlers.Facades.Rates.Tariffs;
using Unfield.Handlers.Facades.Settings.Breaks;
using Unfield.Handlers.Facades.Settings.Main;
using Unfield.Handlers.Mappings;
using Unfield.Handlers.Processors.BookingForm;
using Unfield.Handlers.Resolvers.Customers;
using Unfield.Jobs.Infrastructure.Extensions;
using Unfield.Services.Extensions;

namespace Unfield.Handlers.Extensions;

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
        services.AddScoped<IChangeStadiumGroupFacade, ChangeStadiumGroupFacade>();
        services.AddScoped<IUpdateCustomerFacade, UpdateCustomerFacade>();

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
        
        services.AddScoped<IConfirmBookingFacade, ConfirmBookingFacade>();
        services.AddScoped<ICancelBookingFacade, CancelBookingFacade>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        MapperConfiguration mappingConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile( new CommonProfile() );
                cfg.AddProfile( new UtilsProfile( serviceProvider.GetService<EnvConfig>() ) );
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