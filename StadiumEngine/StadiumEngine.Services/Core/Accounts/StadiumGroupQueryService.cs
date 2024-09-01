using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;

namespace StadiumEngine.Services.Core.Accounts;

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