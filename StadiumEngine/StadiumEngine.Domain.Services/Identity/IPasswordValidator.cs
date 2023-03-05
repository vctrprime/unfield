namespace StadiumEngine.Domain.Services.Identity;

public interface IPasswordValidator
{
    bool Validate( string password );
}