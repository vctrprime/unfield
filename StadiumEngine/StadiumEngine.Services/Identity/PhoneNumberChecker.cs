using System.Text.RegularExpressions;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Services.Identity;

internal class PhoneNumberChecker : IPhoneNumberChecker
{
    private readonly List<string> _startWithCheckList =
        new()
        {
            "0",
            "1",
            "2",
            "5",
            "6",
            "7",
            "89",
            "80"
        };

    public string Check( string phoneNumber )
    {
        if ( String.IsNullOrEmpty( phoneNumber )
             || String.IsNullOrWhiteSpace( phoneNumber ) )
        {
            throw new DomainException( ErrorsKeys.IncorrectPhoneNumber );
        }

        Regex digitsRegex = new( "[^0-9]" );

        string phoneDigits = digitsRegex.Replace( phoneNumber, String.Empty );

        // Проверим количество оставшихся цифр
        if ( phoneDigits.Length < 10 || phoneDigits.Length > 11 )
        {
            throw new DomainException( ErrorsKeys.IncorrectPhoneNumber );
        }

        // Возьмём 10 цифр с конца
        string phone = phoneDigits.Substring( phoneDigits.Length - 10, 10 );

        if ( _startWithCheckList.Any( checkValue => phone.StartsWith( checkValue ) ) )
        {
            throw new DomainException( ErrorsKeys.IncorrectPhoneNumber );
        }

        phone = phone.StartsWith( "7" ) ? phone : $"7{phone}";

        return phone;
    }
}