using System.Security.Claims;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Services.Identity;

internal class CustomerClaimsIdentityService : ICustomerClaimsIdentityService
{
    private readonly ClaimsPrincipal _userIdentity;

    public CustomerClaimsIdentityService( ClaimsPrincipal userIdentity )
    {
        _userIdentity = userIdentity;
    }

    public int GetCustomerId() => GetClaim<int>( "id", "0" );

    public string GetCustomerPhoneNumber() => GetClaim<string>( "phoneNumber" );

    private T GetClaim<T>( string claimName, string? emptyValue = null )
    {
        string value = _userIdentity.FindFirst( claimName )?.Value ?? emptyValue ?? String.Empty;
        
        return ( T )Convert.ChangeType( value, typeof( T ) );
    }
}