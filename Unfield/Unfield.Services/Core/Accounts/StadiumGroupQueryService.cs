using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Services.Core.Accounts;

namespace Unfield.Services.Core.Accounts;

internal class StadiumGroupQueryService : IStadiumGroupQueryService
{
    private readonly IStadiumGroupRepository _stadiumGroupRepository;

    public StadiumGroupQueryService(
        IStadiumGroupRepository stadiumGroupRepository )
    {
        _stadiumGroupRepository = stadiumGroupRepository;
    }

    public async Task<List<StadiumGroup>> GetStadiumGroupsByFilterAsync( string searchString ) =>
        await _stadiumGroupRepository.GetByFilterAsync( searchString );
}