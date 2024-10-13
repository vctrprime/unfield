using Microsoft.Extensions.DependencyInjection;
using Unfield.Common.Configuration;
using Unfield.Common.Configuration.Sections;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Core.BookingForm;
using Unfield.Domain.Services.Core.BookingForm.Builders;
using Unfield.Domain.Services.Core.BookingForm.Distributors;
using Unfield.Domain.Services.Core.Customers;
using Unfield.Domain.Services.Core.Dashboard;
using Unfield.Domain.Services.Core.Geo;
using Unfield.Domain.Services.Core.Notifications;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Core.Rates;
using Unfield.Domain.Services.Core.Schedule;
using Unfield.Domain.Services.Core.Settings;
using Unfield.Domain.Services.Identity;
using Unfield.Domain.Services.Infrastructure;
using Unfield.Domain.Services.Utils;
using Unfield.EventBus;
using Unfield.Repositories.Infrastructure.Extensions;
using Unfield.Services.Builders.Utils;
using Unfield.Services.Checkers;
using Unfield.Services.Core.Accounts;
using Unfield.Services.Core.Accounts.Decorators;
using Unfield.Services.Core.BookingForm;
using Unfield.Services.Core.BookingForm.Builders;
using Unfield.Services.Core.BookingForm.Distributors;
using Unfield.Services.Core.Customers;
using Unfield.Services.Core.Dashboard;
using Unfield.Services.Facades.Accounts;
using Unfield.Services.Core.Geo;
using Unfield.Services.Core.Offers;
using Unfield.Services.Core.Rates;
using Unfield.Services.Core.Schedule;
using Unfield.Services.Facades.BookingForm;
using Unfield.Services.Facades.Bookings;
using Unfield.Services.Facades.Offers;
using Unfield.Services.Facades.Rates;
using Unfield.Services.Core.Settings;
using Unfield.Services.Facades.Customers;
using Unfield.Services.Handlers.Offers;
using Unfield.Services.Identity;
using Unfield.Services.Infrastructure;
using Unfield.Services.Notifications;
using Unfield.Services.Notifications.Resolvers;
using Unfield.Services.Resolvers.Offers;
using Unfield.Services.Utils;
using Unfield.Services.Validators.Bookings;
using Unfield.Services.Validators.Rates;
using Unfield.Services.Validators.Settings;

namespace Unfield.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices( 
        this IServiceCollection services, 
        ConnectionsConfig connectionsConfig )
    {
        services.RegisterDataAccessModules( connectionsConfig );
        services.RegisterDispatcher();
        
        services.AddSignalR();

        services.AddScoped<IClaimsIdentityService, ClaimsIdentityService>();
        services.AddScoped<ICustomerClaimsIdentityService, CustomerClaimsIdentityService>();
        services.AddScoped<IPasswordGenerator, PasswordGenerator>();
        services.AddScoped<IHasher, Hasher>();
        services.AddScoped<ISmsSender, SmsSender>();
        services.AddScoped<IPhoneNumberChecker, PhoneNumberChecker>();
        services.AddScoped<IPasswordValidator, PasswordValidator>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IMessagePublisher, MessagePublisher>();
        services.AddScoped<ICacheProvider, MemoryCacheProvider>();

        #region core

        services.AddScoped<IUserServiceFacade, UserServiceFacade>();
        services.AddScoped<IUserRepositoryFacade, UserRepositoryFacade>();
        services.AddScoped<IRoleRepositoryFacade, RoleRepositoryFacade>();
        services.AddScoped<IUserQueryService, UserQueryService>();
        services.Decorate<IUserQueryService, UserQueryServiceCacheDecorator>();
        services.AddScoped<IUserCommandService, UserCommandService>();
        services.AddScoped<IStadiumGroupQueryService, StadiumGroupQueryService>();
        services.AddScoped<IStadiumGroupCommandService, StadiumGroupCommandService>();
        services.AddScoped<IRoleQueryService, RoleQueryService>();
        services.AddScoped<IRoleCommandService, RoleCommandService>();
        services.AddScoped<IPermissionCommandService, PermissionCommandService>();
        services.AddScoped<IStadiumQueryService, StadiumQueryService>();

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

        services.AddScoped<IBookingFacade, BookingFacade>();
        services.AddScoped<ISchedulerQueryService, SchedulerQueryService>();
        services.AddScoped<ISchedulerBookingCommandService, SchedulerBookingCommandService>();
        services.AddScoped<ISchedulerBookingQueryService, SchedulerBookingQueryService>();

        services.AddScoped<IUIMessageQueryService, UIMessageQueryService>();
        services.AddScoped<IUIMessageCommandService, UIMessageCommandService>();
        services.AddScoped<IUIMessageLastReadQueryService, UIMessageLastReadQueryService>();
        services.AddScoped<IUIMessageLastReadCommandService, UIMessageLastReadCommandService>();

        services.AddScoped<IStadiumDashboardCommandService, StadiumDashboardCommandService>();
        services.AddScoped<IStadiumDashboardQueryService, StadiumDashboardQueryService>();
        
        services.AddScoped<ICustomerQueryService, CustomerQueryService>();
        services.AddScoped<ICustomerCommandService, CustomerCommandService>();
        services.AddScoped<ICustomerBookingQueryService, CustomerBookingQueryService>();

        services.AddScoped<ICustomerFacade, CustomerFacade>();
        
        #endregion

        #region builders

        services.AddScoped<INewStadiumGroupBuilder, NewStadiumGroupBuilder>();
        services.AddScoped<ICustomerAccountRedirectUrlBuilder, CustomerAccountRedirectUrlBuilder>();

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

        #region resolvers

        services.AddScoped<ILockerRoomStatusResolver, LockerRoomStatusResolver>();
        services.AddScoped<ISmsTemplateResolver, SmsTemplateResolver>();

        #endregion

        #region distributors

        services.AddScoped<IBookingLockerRoomDistributor, BookingLockerRoomDistributor>();

        #endregion

        #region processors

        services.AddScoped<IConfirmBookingRedirectProcessor, ConfirmBookingRedirectProcessor>();

        #endregion

        services.AddScoped<IUINotificationService, UINotificationService>();
        services.AddScoped<IUtilService, UtilService>();
    }
}