#nullable enable
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface IUserQueryFacade
{
    Task<User?> GetUser(int userId);
    Task<List<User>> GetUsersByLegalId(int legalId);
    Task<List<Permission>> GetUserPermissions(int userId);
    Task<List<Stadium>> GetUserStadiums(int userId, int legalId);
}