#nullable enable
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IUserStadiumRepository
{
    Task<UserStadium?> GetAsync( int userId, int stadiumId );
    void Add( UserStadium userStadium );
    void Remove( UserStadium userStadium );
}