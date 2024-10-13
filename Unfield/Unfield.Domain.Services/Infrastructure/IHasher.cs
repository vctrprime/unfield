namespace Unfield.Domain.Services.Infrastructure;

public interface IHasher
{
    string Crypt( string value );

    bool Check( string secretValue, string value );
}