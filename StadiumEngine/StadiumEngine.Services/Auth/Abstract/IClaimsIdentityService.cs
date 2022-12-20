using System.Security.Claims;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Services.Auth.Abstract;

public interface IClaimsIdentityService
{
    int GetUserId();
    
    string GetUserName();
    
    int? GetRoleId();
    
    int GetLegalId();
    
    bool GetIsSuperuser();
}