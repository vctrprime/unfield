using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Facades.Geo;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Domain.Services.Facades.Settings;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.Repositories.Infrastructure.Extensions;
using StadiumEngine.Services.Builders.Utils;
using StadiumEngine.Services.Checkers;
using StadiumEngine.Services.Facades.Accounts;
using StadiumEngine.Services.Facades.Geo;
using StadiumEngine.Services.Facades.Offers;
using StadiumEngine.Services.Facades.Rates;
using StadiumEngine.Services.Facades.Services.Accounts;
using StadiumEngine.Services.Facades.Services.Offers;
using StadiumEngine.Services.Facades.Services.Rates;
using StadiumEngine.Services.Facades.Settings;
using StadiumEngine.Services.Handlers.Offers;
using StadiumEngine.Services.Identity;
using StadiumEngine.Services.Infrastructure;
using StadiumEngine.Services.Validators.Rates;

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

        #region facades

        services.AddScoped<IUserServiceFacade, UserServiceFacade>();
        services.AddScoped<IUserRepositoryFacade, UserRepositoryFacade>();
        services.AddScoped<IRoleRepositoryFacade, RoleRepositoryFacade>();
        services.AddScoped<IUserQueryFacade, UserQueryFacade>();
        services.AddScoped<IUserCommandFacade, UserCommandFacade>();
        services.AddScoped<ILegalQueryFacade, LegalQueryFacade>();
        services.AddScoped<ILegalCommandFacade, LegalCommandFacade>();
        services.AddScoped<IRoleQueryFacade, RoleQueryFacade>();
        services.AddScoped<IRoleCommandFacade, RoleCommandFacade>();
        services.AddScoped<IPermissionCommandFacade, PermissionCommandFacade>();

        services.AddScoped<IFieldServiceFacade, FieldServiceFacade>();
        services.AddScoped<IFieldQueryFacade, FieldQueryFacade>();
        services.AddScoped<IFieldCommandFacade, FieldCommandFacade>();
        services.AddScoped<ILockerRoomQueryFacade, LockerRoomQueryFacade>();
        services.AddScoped<ILockerRoomCommandFacade, LockerRoomCommandFacade>();
        services.AddScoped<IInventoryQueryFacade, InventoryQueryFacade>();
        services.AddScoped<IInventoryCommandFacade, InventoryCommandFacade>();

        services.AddScoped<IPriceGroupQueryFacade, PriceGroupQueryFacade>();
        services.AddScoped<IPriceGroupCommandFacade, PriceGroupCommandFacade>();
        services.AddScoped<ITariffQueryFacade, TariffQueryFacade>();
        services.AddScoped<ITariffCommandFacade, TariffCommandFacade>();
        services.AddScoped<IPriceQueryFacade, PriceQueryFacade>();
        services.AddScoped<IPriceCommandFacade, PriceCommandFacade>();
        services.AddScoped<ITariffRepositoryFacade, TariffRepositoryFacade>();
        
        services.AddScoped<IStadiumMainSettingsQueryFacade, StadiumMainSettingsQueryFacade>();
        services.AddScoped<IStadiumMainSettingsCommandFacade, StadiumMainSettingsCommandFacade>();

        services.AddScoped<ICityQueryFacade, CityQueryFacade>();

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

        #endregion
    }
}