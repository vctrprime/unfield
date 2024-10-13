#nullable enable
namespace Unfield.Domain.Services.Identity;

public interface IClaimsIdentityService
{
    int GetUserId();
    int GetStadiumGroupId();
    int GetCurrentStadiumId();
}