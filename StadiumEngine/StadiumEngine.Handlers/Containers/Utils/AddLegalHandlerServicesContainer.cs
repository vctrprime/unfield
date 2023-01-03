using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Handlers.Containers.Utils;

internal class AddLegalHandlerServicesContainer
{
    public IPasswordGenerator PasswordGenerator { get; set; }
    
}