using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Services.Core.Accounts;

public interface IStadiumQueryService
{
    Task<List<Stadium>> GetAsync( int skip, int take );
    Task<Stadium?> GetAsync( string token );
}