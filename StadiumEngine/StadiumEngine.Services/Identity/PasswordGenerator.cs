using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Services.Identity;

internal class PasswordGenerator : IPasswordGenerator
{
    public string Generate( int length )
    {
        string[] randomChars =
        {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ", // uppercase 
            "abcdefghijkmnopqrstuvwxyz", // lowercase
            "0123456789" // digits
        };

        Random rand = new();
        List<char> chars = new();

        chars.Insert(
            rand.Next( 0, chars.Count ),
            randomChars[ 0 ][ rand.Next( 0, randomChars[ 0 ].Length ) ] );
        chars.Insert(
            rand.Next( 0, chars.Count ),
            randomChars[ 1 ][ rand.Next( 0, randomChars[ 1 ].Length ) ] );
        chars.Insert(
            rand.Next( 0, chars.Count ),
            randomChars[ 2 ][ rand.Next( 0, randomChars[ 2 ].Length ) ] );


        for ( int i = chars.Count;
             i < length
             || chars.Distinct().Count() < length;
             i++ )
        {
            string rcs = randomChars[ rand.Next( 0, randomChars.Length ) ];
            chars.Insert(
                rand.Next( 0, chars.Count ),
                rcs[ rand.Next( 0, rcs.Length ) ] );
        }

        return new string( chars.ToArray() );
    }
}