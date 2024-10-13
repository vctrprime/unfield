using System.Collections.Generic;

namespace Unfield.Domain.Entities.Dashboard;

public class StadiumDashboardFieldDistribution
{
    public List<StadiumDashboardFieldDistributionItem> Items { get; set; }
}

public class StadiumDashboardFieldDistributionItem 
{
    public string Field { get; set; }
    public int Value { get; set; }
}