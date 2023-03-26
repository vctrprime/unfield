#nullable enable
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface IUserQueryFacade
{
    Task<User?> GetUserAsync( int userId );
    Task<List<User>> GetUsersByLegalIdAsync( int legalId );
    Task<List<Permission>> GetUserPermissionsAsync( int userId );
    Task<List<Stadium>> GetUserStadiumsAsync( int userId, int legalId );
}