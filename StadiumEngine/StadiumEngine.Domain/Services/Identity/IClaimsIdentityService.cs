#nullable enable
namespace StadiumEngine.Domain.Services.Identity;

public interface IClaimsIdentityService
{
    int GetUserId();
    
    string GetUserName();
    
    int GetRoleId();

    string GetRoleName();
    
    int GetLegalId();
    
    bool GetIsSuperuser();

    int GetCurrentStadiumId();
}