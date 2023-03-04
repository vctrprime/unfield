#nullable enable
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface IUserCommandFacade
{
    Task<User> AuthorizeUser(string login, string password);
    Task<User> ChangeStadium(int userId, int stadiumId);
    Task ChangeLegal(int userId, int legalId);
    Task<string> AddUser(User user, bool isAdminUser = false);
    Task ChangeLanguage(int userId, string language);
    Task ChangePassword(
        int userId,
        string newPassword,
        string oldPassword);
    Task<(User, string)> ResetPassword(string phoneNumber);
    Task UpdateUser(User user, int legalId);
    Task DeleteUser(int userId, int legalId, int userModifiedId);
}