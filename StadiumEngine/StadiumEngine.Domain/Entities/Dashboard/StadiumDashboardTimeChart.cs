using System.Collections.Generic;

namespace StadiumEngine.Domain.Entities.Dashboard;

public class StadiumDashboardTimeChart
{
    public List<StadiumDashboardTimeChartItem> Items { get; set; }
}

public class StadiumDashboardTimeChartItem
{
    public string Time { get; set; }
    public int Value { get; set; }
}