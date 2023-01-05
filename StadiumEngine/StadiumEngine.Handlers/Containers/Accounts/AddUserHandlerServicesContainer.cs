using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Handlers.Containers.Accounts;

internal class AddUserHandlerServicesContainer
{
    public IPasswordGenerator PasswordGenerator { get; set; }
    public IHasher Hasher { get; set; }
    public ISmsSender SmsSender { get; set; }
    
    public IPhoneNumberChecker PhoneNumberChecker { get; set; }
}