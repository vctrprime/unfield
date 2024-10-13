#nullable enable
namespace Unfield.Domain.Services.Identity;

public interface ICustomerClaimsIdentityService
{
    int GetCustomerId();
    string GetCustomerPhoneNumber();
    int GetCustomerStadiumGroupId();
}