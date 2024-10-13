namespace Unfield.Domain.Services.Identity;

public interface IPasswordValidator
{
    bool Validate( string password );
}