#nullable enable
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Repositories.Accounts;

public interface IUserStadiumRepository
{
    Task<UserStadium?> GetAsync( int userId, int stadiumId );
    void Add( UserStadium userStadium );
    void Remove( UserStadium userStadium );
}