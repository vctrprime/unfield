using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Core.BookingForm;
using StadiumEngine.Domain.Services.Core.Geo;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Domain.Services.Core.Rates;
using StadiumEngine.Domain.Services.Core.Schedule;
using StadiumEngine.Domain.Services.Core.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.Repositories.Infrastructure.Extensions;
using StadiumEngine.Services.Builders.Utils;
using StadiumEngine.Services.Checkers;
using StadiumEngine.Services.Core.Accounts;
using StadiumEngine.Services.Core.BookingForm;
using StadiumEngine.Services.Facades.Accounts;
using StadiumEngine.Services.Core.Geo;
using StadiumEngine.Services.Core.Offers;
using StadiumEngine.Services.Core.Rates;
using StadiumEngine.Services.Core.Schedule;
using StadiumEngine.Services.Facades.BookingForm;
using StadiumEngine.Services.Facades.Bookings;
using StadiumEngine.Services.Facades.Offers;
using StadiumEngine.Services.Facades.Rates;
using StadiumEngine.Services.Core.Settings;
using StadiumEngine.Services.Handlers.Offers;
using StadiumEngine.Services.Identity;
using StadiumEngine.Services.Infrastructure;
using StadiumEngine.Services.Validators.Bookings;
using StadiumEngine.Services.Validators.Rates;
using StadiumEngine.Services.Validators.Settings;

namespace StadiumEngine.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices( this IServiceCollection services )
    {
        services.RegisterDataAccessModules();

        services.AddScoped<IClaimsIdentityService, ClaimsIdentityService>();
        services.AddScoped<IPasswordGenerator, PasswordGenerator>();
        services.AddScoped<IHasher, Hasher>();
        services.AddScoped<ISmsSender, SmsSender>();
        services.AddScoped<IPhoneNumberChecker, PhoneNumberChecker>();
        services.AddScoped<IPasswordValidator, PasswordValidator>();
        services.AddScoped<IImageService, ImageService>();

        #region application

        services.AddScoped<IUserServiceFacade, UserServiceFacade>();
        services.AddScoped<IUserRepositoryFacade, UserRepositoryFacade>();
        services.AddScoped<IRoleRepositoryFacade, RoleRepositoryFacade>();
        services.AddScoped<IUserQueryService, UserQueryService>();
        services.AddScoped<IUserCommandService, UserCommandService>();
        services.AddScoped<ILegalQueryService, LegalQueryService>();
        services.AddScoped<ILegalCommandService, LegalCommandService>();
        services.AddScoped<IRoleQueryService, RoleQueryService>();
        services.AddScoped<IRoleCommandService, RoleCommandService>();
        services.AddScoped<IPermissionCommandService, PermissionCommandService>();

        services.AddScoped<IFieldServiceFacade, FieldServiceFacade>();
        services.AddScoped<IFieldQueryService, FieldQueryService>();
        services.AddScoped<IFieldCommandService, FieldCommandService>();
        services.AddScoped<ILockerRoomQueryService, LockerRoomQueryService>();
        services.AddScoped<ILockerRoomCommandService, LockerRoomCommandService>();
        services.AddScoped<IInventoryQueryService, InventoryQueryService>();
        services.AddScoped<IInventoryCommandService, InventoryCommandService>();

        services.AddScoped<IPriceGroupQueryService, PriceGroupQueryService>();
        services.AddScoped<IPriceGroupCommandService, PriceGroupCommandService>();
        services.AddScoped<ITariffQueryService, TariffQueryService>();
        services.AddScoped<ITariffCommandService, TariffCommandService>();
        services.AddScoped<IPriceQueryService, PriceQueryService>();
        services.AddScoped<IPriceCommandService, PriceCommandService>();
        services.AddScoped<ITariffRepositoryFacade, TariffRepositoryFacade>();
        
        services.AddScoped<IMainSettingsQueryService, MainSettingsQueryService>();
        services.AddScoped<IMainSettingsCommandService, MainSettingsCommandService>();
        services.AddScoped<IBreakQueryService, BreakQueryService>();
        services.AddScoped<IBreakCommandService, BreakCommandService>();
        
        services.AddScoped<ICityQueryService, CityQueryService>();
        
        services.AddScoped<IBookingFormQueryService, BookingFormQueryService>();
        services.AddScoped<IBookingFormCommandService, BookingFormCommandService>();
        services.AddScoped<IBookingFormFieldRepositoryFacade, BookingFormFieldRepositoryFacade>();
        services.AddScoped<IBookingCheckoutQueryService, BookingCheckoutQueryService>();
        services.AddScoped<IBookingCheckoutCommandService, BookingCheckoutCommandService>();
        services.AddScoped<IBookingRepositoriesFacade, BookingRepositoriesFacade>();

        services.AddScoped<IBookingFacade, BookingFacade>();
        services.AddScoped<ISchedulerQueryService, SchedulerQueryService>();
        
        #endregion

        #region builders

        services.AddScoped<INewLegalBuilder, NewLegalBuilder>();

        #endregion

        #region checkers

        services.AddScoped<IAccountsAccessChecker, AccountsAccessChecker>();

        #endregion

        #region handlers
        
        services.AddScoped<IFieldPriceGroupHandler, FieldPriceGroupHandler>();
        
        #endregion

        #region validators

        services.AddScoped<ITariffValidator, TariffValidator>();
        services.AddScoped<IBookingIntersectionValidator, BookingIntersectionValidator>();
        services.AddScoped<IBreakValidator, BreakValidator>();

        #endregion
        
        
    }
}