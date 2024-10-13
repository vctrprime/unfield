namespace Unfield.Domain.Services.Identity;

public interface IPasswordGenerator
{
    string Generate( int length );
}