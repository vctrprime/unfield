using StadiumEngine.Domain.Entities.Dashboard;
using StadiumEngine.Domain.Repositories.Dashboard;
using StadiumEngine.Domain.Services.Core.Dashboard;

namespace StadiumEngine.Services.Core.Dashboard;

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