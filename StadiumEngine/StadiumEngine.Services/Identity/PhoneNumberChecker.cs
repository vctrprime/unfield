using System.Text.RegularExpressions;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Services.Identity;

internal class PhoneNumberChecker : IPhoneNumberChecker
{
    private readonly List<string> _startWithCheckList =
        new List<string>()
        {
            "0",
            "1",
            "2",
            "5",
            "6",
            "7",
            "89",
            "80",
        };
    
    public string Check(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber)
            || string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new DomainException(ErrorsKeys.IncorrectPhoneNumber);
        }
        
        var digitsRegex = new Regex("[^0-9]");

        var phoneDigits = digitsRegex.Replace(phoneNumber, string.Empty);

        // Проверим количество оставшихся цифр
        if (phoneDigits.Length < 10 || phoneDigits.Length > 11)
        {
            throw new DomainException(ErrorsKeys.IncorrectPhoneNumber);
        }
            
        // Возьмём 10 цифр с конца
        var phone = phoneDigits.Substring(phoneDigits.Length - 10, 10);

        if (_startWithCheckList.Any(checkValue => phone.StartsWith(checkValue)))
        {
            throw new DomainException(ErrorsKeys.IncorrectPhoneNumber);
        }
            
        phone = phone.StartsWith("7") ? phone : $"7{phone}";

        return phone;
    }
    
}