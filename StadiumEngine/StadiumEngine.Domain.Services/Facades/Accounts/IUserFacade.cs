#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface IUserFacade
{
    Task<User> AuthorizeUser(string login, string password);
    Task<User> ChangeStadium(int userId, int stadiumId);
    Task ChangeLegal(int userId, int legalId);
    Task<string> AddUser(User user, bool isAdminUser = false);
    Task<User?> GetUser(int userId);
    Task<List<User>> GetUsersByLegalId(int legalId);
    Task ChangeLanguage(int userId, string language);
    Task ChangePassword(
        int userId,
        string newPassword,
        string oldPassword);
    Task<(User, string)> ResetPassword(string phoneNumber);
    Task UpdateUser(User user, int legalId);
    Task DeleteUser(int userId, int legalId, int userModifiedId);
    Task<List<Permission>> GetUserPermissions(int userId);
    Task<List<Stadium>> GetUserStadiums(int userId, int legalId);
}