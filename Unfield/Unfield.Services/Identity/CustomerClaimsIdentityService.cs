using System.Security.Claims;
using Unfield.Domain.Services.Identity;

namespace Unfield.Services.Identity;

internal class CustomerClaimsIdentityService : ICustomerClaimsIdentityService
{
    private readonly ClaimsPrincipal _userIdentity;

    public CustomerClaimsIdentityService( ClaimsPrincipal userIdentity )
    {
        _userIdentity = userIdentity;
    }

    public int GetCustomerId() => GetClaim<int>( "customerId", "0" );
    
    public int GetCustomerStadiumGroupId() => GetClaim<int>( "customerStadiumGroupId", "0" );

    public string GetCustomerPhoneNumber() => GetClaim<string>( "customerPhoneNumber" );

    private T GetClaim<T>( string claimName, string? emptyValue = null )
    {
        string value = _userIdentity.FindFirst( claimName )?.Value ?? emptyValue ?? String.Empty;
        
        return ( T )Convert.ChangeType( value, typeof( T ) );
    }
}