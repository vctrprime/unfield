using Unfield.Common.Configuration.Sections;
using Unfield.Domain.Services.Core.BookingForm.Builders;

namespace Unfield.Services.Core.BookingForm.Builders;

internal class CustomerAccountRedirectUrlBuilder : ICustomerAccountRedirectUrlBuilder
{
    private readonly EnvConfig _envConfig;

    public CustomerAccountRedirectUrlBuilder( EnvConfig envConfig )
    {
        _envConfig = envConfig;
    }

    public string? Build( string? token, string language )
    {
        if ( String.IsNullOrEmpty( token ) )
        {
            return null;
        }
        
        return _envConfig.CustomerAccountHost + "/redirect/" + $"{language}/" + token;
    }
}