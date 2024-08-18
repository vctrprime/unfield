using StadiumEngine.Domain.Entities.Dashboard;

namespace StadiumEngine.BackgroundWorker.Builders.Dashboard;

internal class StadiumDashboardDataBuilder : IStadiumDashboardDataBuilder
{
    public async Task<StadiumDashboard> BuildAsync( int stadiumId, DateTime date )
    {
        // todo логика билда дашборда
        Console.WriteLine( $"Build stadium {stadiumId}");
        return new StadiumDashboard
        {
            StadiumId = stadiumId,
            Data = new StadiumDashboardData
            {
                YearChart = new StadiumDashboardYearChart
                {
                    Items =
                    [
                        new StadiumDashboardYearChartItem
                        {
                            Month = "January",
                            Value = 1,
                        },

                        new StadiumDashboardYearChartItem
                        {
                            Month = "February",
                            Value = 2
                        }
                    ]
                },
                FieldDistribution = new StadiumDashboardFieldDistribution
                {
                    Items =
                    [
                        new StadiumDashboardFieldDistributionItem
                        {
                            Field = "Field 1",
                            Value = 1,
                        },

                        new StadiumDashboardFieldDistributionItem
                        {
                            Field = "Field 2",
                            Value = 2
                        }
                    ]
                },
                AverageBill = new StadiumDashboardAverageBill
                {
                    FieldValue = 100,
                    InventoryValue = ( decimal )200.55,
                    TotalValue = 300
                },
                PopularInventory = new StadiumDashboardPopularInventory
                {
                    Items =
                    [
                        new StadiumDashboardPopularInventoryItem
                        {
                            Inventory = "Inventory 1",
                            Value = 1,
                        },

                        new StadiumDashboardPopularInventoryItem
                        {
                            Inventory = "Inventory 2",
                            Value = 2
                        }
                    ]
                },
                TimeChart = new StadiumDashboardTimeChart
                {
                    Items =
                    [
                        new StadiumDashboardTimeChartItem
                        {
                            Time = "00:00",
                            Value = 1,
                        },

                        new StadiumDashboardTimeChartItem
                        {
                            Time = "01:00",
                            Value = 2
                        }
                    ]
                }
            }
        };
    }
}