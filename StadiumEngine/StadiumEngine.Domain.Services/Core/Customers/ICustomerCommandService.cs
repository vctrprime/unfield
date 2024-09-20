using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Services.Models.Customers;

namespace StadiumEngine.Domain.Services.Core.Customers;

public interface ICustomerCommandService
{
    Task<Customer> AuthorizeCustomerAsync( string login, string stadiumToken, string password );
    Task ChangeLanguageAsync( int customerId, string language );
    Task ChangePasswordAsync(
        int customerId,
        string newPassword,
        string oldPassword );

    Task ResetPasswordAsync( string phoneNumber, string stadiumToken );
    Task RegisterAsync( CreateCustomerData createCustomerData );
    void Update( Customer customer );
}