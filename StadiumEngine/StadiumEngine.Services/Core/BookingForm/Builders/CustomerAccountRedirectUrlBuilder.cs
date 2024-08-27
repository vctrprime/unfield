using StadiumEngine.Common.Configuration.Sections;
using StadiumEngine.Domain.Services.Core.BookingForm.Builders;

namespace StadiumEngine.Services.Core.BookingForm.Builders;

internal class CustomerAccountRedirectUrlBuilder : ICustomerAccountRedirectUrlBuilder
{
    private readonly EnvConfig _envConfig;

    public CustomerAccountRedirectUrlBuilder( EnvConfig envConfig )
    {
        _envConfig = envConfig;
    }

    public string? Build( string? token )
    {
        if ( String.IsNullOrEmpty( token ) )
        {
            return null;
        }
        
        return _envConfig.CustomerAccountHost + "/redirect/" + token;
    }
}