using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface ILegalQueryFacade
{
    Task<List<Legal>> GetLegalsByFilterAsync( string searchString );
}