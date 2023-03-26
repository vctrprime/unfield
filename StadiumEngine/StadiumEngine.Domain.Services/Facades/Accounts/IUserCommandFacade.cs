#nullable enable
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface IUserCommandFacade
{
    Task<User> AuthorizeUserAsync( string login, string password );
    Task<User> ChangeStadiumAsync( int userId, int stadiumId );
    Task ChangeLegalAsync( int userId, int legalId );
    Task<string> AddUserAsync( User user, bool isAdminUser = false );
    Task ChangeLanguageAsync( int userId, string language );

    Task ChangePasswordAsync(
        int userId,
        string newPassword,
        string oldPassword );

    Task<(User, string)> ResetPasswordAsync( string phoneNumber );
    Task UpdateUserAsync( User user, int legalId );
    Task DeleteUserAsync( int userId, int legalId, int userModifiedId );
}