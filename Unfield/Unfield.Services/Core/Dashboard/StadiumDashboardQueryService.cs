using Unfield.Domain.Entities.Dashboard;
using Unfield.Domain.Repositories.Dashboard;
using Unfield.Domain.Services.Core.Dashboard;

namespace Unfield.Services.Core.Dashboard;

internal class StadiumDashboardQueryService : IStadiumDashboardQueryService
{
    private readonly IStadiumDashboardRepository _repository;

    public StadiumDashboardQueryService( IStadiumDashboardRepository repository )
    {
        _repository = repository;
    }
    

    public async Task<StadiumDashboard?> GetAsync( int stadiumId ) => 
        await _repository.GetAsync( stadiumId);
}