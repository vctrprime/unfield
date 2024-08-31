namespace StadiumEngine.Jobs.Background.Dashboard;

internal interface IStadiumDashboardCalculator
{
    Task CalculateAsync( int stadiumId );
}