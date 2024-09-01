#nullable enable
namespace StadiumEngine.Domain.Services.Identity;

public interface IClaimsIdentityService
{
    int GetUserId();
    int GetStadiumGroupId();
    int GetCurrentStadiumId();
}