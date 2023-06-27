using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Application.Accounts;

public interface ILegalQueryService
{
    Task<List<Legal>> GetLegalsByFilterAsync( string searchString );
}