using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IStadiumRepository
{ 
    Task<List<Stadium>> GetAsync( int skip, int take );
    Task<Stadium?> GetAsync( int id );
    Task<List<Stadium>> GetForStadiumGroupAsync( int stadiumGroupId );
    Task<List<Stadium>> GetForUserAsync( int userId );
    Task<Stadium?> GetByTokenAsync( string token );
}