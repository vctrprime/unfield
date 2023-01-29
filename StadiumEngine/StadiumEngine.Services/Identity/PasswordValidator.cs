using System.Text.RegularExpressions;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Services.Identity;

internal class PasswordValidator : IPasswordValidator
{
    public bool Validate(string password)
    {
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMinimum8Chars = new Regex(@".{8,}");
        
        return hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
    }
}