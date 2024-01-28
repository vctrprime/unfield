using StadiumEngine.BackgroundWorker.Builders.Dashboard;

namespace StadiumEngine.BackgroundWorker.Jobs.Dashboard;

internal class DashboardCalculatorJob : IDashboardCalculatorJob
{
    private readonly IDashboardDataBuilder _builder;

    public DashboardCalculatorJob( IDashboardDataBuilder builder )
    {
        _builder = builder;
    }

    public async Task Calculate()
    {
        foreach ( int i in new List<int>()
                 {
                     1, 2, 5, 16, 17, 20
                 } )
        {
            await _builder.BuildAsync( i, DateTime.Now );
        }
    }
}