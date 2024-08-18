using System.Collections.Generic;

namespace StadiumEngine.Domain.Entities.Dashboard;

public class StadiumDashboardYearChart
{
    public List<StadiumDashboardYearChartItem> Items { get; set; }
}

public class StadiumDashboardYearChartItem 
{
    public string Month { get; set; }
    public int Value { get; set; }
}