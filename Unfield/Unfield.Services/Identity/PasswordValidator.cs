using System.Text.RegularExpressions;
using Unfield.Domain.Services.Identity;

namespace Unfield.Services.Identity;

internal class PasswordValidator : IPasswordValidator
{
    public bool Validate( string password )
    {
        Regex hasNumber = new( @"[0-9]+" );
        Regex hasUpperChar = new( @"[A-Z]+" );
        Regex hasMinimum8Chars = new( @".{8,}" );

        return hasNumber.IsMatch( password ) && hasUpperChar.IsMatch( password ) &&
               hasMinimum8Chars.IsMatch( password );
    }
}