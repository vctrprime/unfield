using Unfield.Domain.Entities.Accounts;

namespace Unfield.Domain.Services.Core.Accounts;

public interface IStadiumGroupQueryService
{
    Task<List<StadiumGroup>> GetStadiumGroupsByFilterAsync( string searchString );
}