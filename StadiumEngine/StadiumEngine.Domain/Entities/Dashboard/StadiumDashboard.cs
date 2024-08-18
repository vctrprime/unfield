
namespace StadiumEngine.Domain.Entities.Dashboard;

public class StadiumDashboard : BaseEntity
{
    public StadiumDashboardData Data { get; set; }
    
    public int StadiumId { get; set; }
}

public class StadiumDashboardData
{
    public StadiumDashboardYearChart YearChart { get; set; }
    public StadiumDashboardFieldDistribution FieldDistribution { get; set; }
    public StadiumDashboardAverageBill AverageBill { get; set; }
    public StadiumDashboardPopularInventory PopularInventory { get; set; }
    public StadiumDashboardTimeChart TimeChart { get; set; }
}






