using Newtonsoft.Json;
using StadiumEngine.Domain.Entities.Dashboard;
using StadiumEngine.Domain.Repositories.Dashboard;
using StadiumEngine.Domain.Services.Core.Dashboard;
using StadiumEngine.Domain.Services.Models.Dashboard;

namespace StadiumEngine.Services.Core.Dashboard;

internal class StadiumDashboardCommandService : IStadiumDashboardCommandService
{
    private readonly IStadiumDashboardRepository _repository;

    public StadiumDashboardCommandService( IStadiumDashboardRepository repository )
    {
        _repository = repository;
    }

    public async Task AddAsync( StadiumDashboardResult dashboardData ) =>
        await _repository.AddAsync( new StadiumDashboard
        {
            Data = JsonConvert.SerializeObject( dashboardData.Data )
        } );
}