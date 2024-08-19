namespace StadiumEngine.Jobs.Background.Dashboard;

public interface IStadiumDashboardCalculator
{
    Task CalculateAsync( int stadiumId );
}