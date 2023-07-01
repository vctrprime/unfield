using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Core.Accounts;

public interface ILegalQueryService
{
    Task<List<Legal>> GetLegalsByFilterAsync( string searchString );
}