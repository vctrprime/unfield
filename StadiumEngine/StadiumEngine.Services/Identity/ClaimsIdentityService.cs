using System.Security.Claims;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Services.Identity;

internal class ClaimsIdentityService : IClaimsIdentityService
{
    private readonly ClaimsPrincipal _userIdentity;

    public ClaimsIdentityService( ClaimsPrincipal userIdentity )
    {
        _userIdentity = userIdentity;
    }

    public int GetUserId() => GetClaim<int>( "id", "0" );

    public int GetLegalId() => GetClaim<int>( "legalId", "0" );

    public int GetCurrentStadiumId() => GetClaim<int>( "stadiumId", "0" );

    private T GetClaim<T>( string claimName, string? emptyValue = null )
    {
        string value = _userIdentity.FindFirst( claimName )?.Value ?? emptyValue ?? String.Empty;
        
        return ( T )Convert.ChangeType( value, typeof( T ) );
    }
}