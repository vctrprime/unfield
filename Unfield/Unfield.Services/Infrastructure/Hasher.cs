using System.Security.Cryptography;
using Unfield.Domain.Services.Infrastructure;

namespace Unfield.Services.Infrastructure;

internal class Hasher : IHasher
{
    private const int SaltSize = 16; // 128 bit 
    private const int KeySize = 32; // 256 bit
    private const int Iterations = 10000;

    public string Crypt( string value )
    {
        using Rfc2898DeriveBytes algorithm = new(
            value,
            SaltSize,
            Iterations,
            HashAlgorithmName.SHA256 );
        string key = Convert.ToBase64String( algorithm.GetBytes( KeySize ) );
        string salt = Convert.ToBase64String( algorithm.Salt );

        return $"{Iterations}.{salt}.{key}";
    }

    public bool Check( string secretValue, string value )
    {
        string[] parts = secretValue.Split( '.', 3 );

        if ( parts.Length != 3 )
        {
            return false;
        }

        int iterations = Convert.ToInt32( parts[ 0 ] );
        byte[] salt = Convert.FromBase64String( parts[ 1 ] );
        byte[] key = Convert.FromBase64String( parts[ 2 ] );

        using Rfc2898DeriveBytes algorithm = new(
            value,
            salt,
            iterations,
            HashAlgorithmName.SHA256 );
        byte[] keyToCheck = algorithm.GetBytes( KeySize );

        bool verified = keyToCheck.SequenceEqual( key );

        return verified;
    }
}