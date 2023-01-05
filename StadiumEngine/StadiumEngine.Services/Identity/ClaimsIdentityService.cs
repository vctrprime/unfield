using System.Security.Claims;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Services.Identity;

internal class ClaimsIdentityService : IClaimsIdentityService
{
    private readonly ClaimsPrincipal _userIdentity;

    public ClaimsIdentityService(ClaimsPrincipal userIdentity)
    {
        _userIdentity = userIdentity;
    }
    
    public int GetUserId()
    {
        return GetClaim<int>("id");
    }
    
    public int GetLegalId()
    {
        return GetClaim<int>("legalId");
    }
    
    public int GetCurrentStadiumId()
    {
        return GetClaim<int>("stadiumId");
    }

    private T GetClaim<T>(string claimName)
    {
        var value = _userIdentity.FindFirst(claimName)?.Value ?? String.Empty;

        return (T)Convert.ChangeType(value, typeof(T));

    }
}