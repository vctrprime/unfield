#nullable enable
namespace StadiumEngine.Domain.Services.Identity;

public interface ICustomerClaimsIdentityService
{
    int GetCustomerId();
    string GetCustomerPhoneNumber();
    int GetCustomerStadiumId();
}