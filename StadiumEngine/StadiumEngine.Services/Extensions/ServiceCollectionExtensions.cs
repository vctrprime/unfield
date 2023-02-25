using Microsoft.Extensions.DependencyInjection;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.Repositories.Infrastructure.Extensions;
using StadiumEngine.Services.Builders.Utils;
using StadiumEngine.Services.Checkers;
using StadiumEngine.Services.Facades.Accounts;
using StadiumEngine.Services.Facades.Offers;
using StadiumEngine.Services.Facades.Services.Accounts;
using StadiumEngine.Services.Identity;
using StadiumEngine.Services.Infrastructure;

namespace StadiumEngine.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services)
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
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<ILegalFacade, LegalFacade>();
        services.AddScoped<IRoleFacade, RoleFacade>();
        services.AddScoped<IPermissionFacade, PermissionFacade>();
        
        services.AddScoped<IFieldFacade, FieldFacade>();
        services.AddScoped<ILockerRoomFacade, LockerRoomFacade>();

        #endregion

        #region builders
        
        services.AddScoped<INewLegalBuilder, NewLegalBuilder>();
        
        #endregion

        #region checkers

        services.AddScoped<IAccountsAccessChecker, AccountsAccessChecker>();

        #endregion
    }
    
    
}