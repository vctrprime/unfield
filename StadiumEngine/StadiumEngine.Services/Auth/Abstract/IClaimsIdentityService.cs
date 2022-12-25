using System.Security.Claims;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Services.Auth.Abstract;

public interface IClaimsIdentityService
{
    int GetUserId();
    
    string GetUserName();
    
    int? GetRoleId();

    string? GetRoleName();
    
    int GetLegalId();
    
    bool GetIsSuperuser();

    int GetCurrentStadiumId();
}