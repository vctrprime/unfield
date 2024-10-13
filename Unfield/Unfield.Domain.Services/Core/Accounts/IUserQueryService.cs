#nullable enable
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Services.Core.Accounts;

public interface IUserQueryService
{
    Task<User?> GetUserAsync( int userId );
    Task<List<User>> GetUsersByStadiumGroupIdAsync( int stadiumGroupId );
    Task<List<Permission>> GetUserPermissionsAsync( int userId );
    Task<List<Stadium>> GetUserStadiumsAsync( int userId, int stadiumGroupId );
    Task<Dictionary<Stadium, bool>> GetStadiumsForUserAsync( int userId, int stadiumGroupId );
}