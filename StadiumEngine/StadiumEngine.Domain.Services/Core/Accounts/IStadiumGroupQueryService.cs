using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Core.Accounts;

public interface IStadiumGroupQueryService
{
    Task<List<StadiumGroup>> GetStadiumGroupsByFilterAsync( string searchString );
}