#nullable enable
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Services.Core.Accounts;

public interface IUserCommandService
{
    Task<User> AuthorizeUserAsync( string login, string password );
    Task<User> ChangeStadiumAsync( int userId, int stadiumId );
    Task ChangeStadiumGroupAsync( int userId, int stadiumGroupId );
    Task<string> AddUserAsync( User user, bool isAdminUser = false );
    Task ChangeLanguageAsync( int userId, string language );

    Task ChangePasswordAsync(
        int userId,
        string newPassword,
        string oldPassword );

    Task ResetPasswordAsync( string phoneNumber );
    void UpdateUser( User user );
    Task DeleteUserAsync( int userId, int stadiumGroupId, int userModifiedId );
    Task ToggleUserStadiumAsync(
        int userId,
        int stadiumId,
        int stadiumGroupId,
        int modifyUserId );
}