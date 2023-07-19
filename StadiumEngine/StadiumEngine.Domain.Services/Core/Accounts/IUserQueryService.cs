#nullable enable
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Core.Accounts;

public interface IUserQueryService
{
    Task<User?> GetUserAsync( int userId );
    Task<List<User>> GetUsersByLegalIdAsync( int legalId );
    Task<List<Permission>> GetUserPermissionsAsync( int userId );
    Task<List<Stadium>> GetUserStadiumsAsync( int userId, int legalId );
    Task<Dictionary<Stadium, bool>> GetStadiumsForUserAsync( int userId, int legalId );
}