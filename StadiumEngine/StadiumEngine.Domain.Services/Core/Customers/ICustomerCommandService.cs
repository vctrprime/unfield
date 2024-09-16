using StadiumEngine.Domain.Entities.Customers;

namespace StadiumEngine.Domain.Services.Core.Customers;

public interface ICustomerCommandService
{
    Task<Customer> AuthorizeCustomerAsync( string login, int stadiumId, string password );
    Task ChangeLanguageAsync( int customerId, string language );
    Task ChangePasswordAsync(
        int customerId,
        string newPassword,
        string oldPassword );

    Task ResetPasswordAsync( string phoneNumber, int stadiumId );
}