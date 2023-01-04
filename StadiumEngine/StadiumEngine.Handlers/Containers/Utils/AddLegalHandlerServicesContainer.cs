using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Handlers.Containers.Utils;

internal class AddLegalHandlerServicesContainer
{
    public IPasswordGenerator PasswordGenerator { get; set; }
    public IHasher Hasher { get; set; }
    public ISmsSender SmsSender { get; set; }
    
}