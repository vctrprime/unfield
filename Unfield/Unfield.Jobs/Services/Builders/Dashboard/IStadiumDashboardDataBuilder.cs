using Unfield.Domain.Entities.Dashboard;

namespace Unfield.Jobs.Services.Builders.Dashboard;

internal interface IStadiumDashboardDataBuilder
{
    Task<StadiumDashboard> BuildAsync( int stadiumId, DateTime date );
}