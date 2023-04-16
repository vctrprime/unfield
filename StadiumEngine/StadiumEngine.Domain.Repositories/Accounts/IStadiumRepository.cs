using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IStadiumRepository
{
    Task<List<Stadium>> GetForLegalAsync( int legalId );

    Task<List<Stadium>> GetForRoleAsync( int roleId );

    Task<Stadium?> GetByTokenAsync( string token );
}