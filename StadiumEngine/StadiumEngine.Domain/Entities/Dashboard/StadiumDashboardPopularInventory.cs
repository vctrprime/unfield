using System.Collections.Generic;

namespace StadiumEngine.Domain.Entities.Dashboard;

public class StadiumDashboardPopularInventory 
{
    public List<StadiumDashboardPopularInventoryItem> Items { get; set; }
}

public class StadiumDashboardPopularInventoryItem 
{
    public string Inventory { get; set; }
    public decimal Value { get; set; }
}