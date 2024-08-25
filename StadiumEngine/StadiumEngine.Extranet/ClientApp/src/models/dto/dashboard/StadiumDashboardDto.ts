export interface StadiumDashboardDto {
    calculationDate: Date;
    yearChart: StadiumDashboardChartItemDto[];
    averageBill: StadiumDashboardAverageBillDto;
    fieldDistribution: StadiumDashboardChartItemDto[];
    timeChart: StadiumDashboardChartItemDto[];
}

export interface StadiumDashboardChartItemDto {
    category: string;
    value: number;
}

export interface StadiumDashboardAverageBillDto {
    fieldValue: number;
    inventoryValue: number;
    totalValue: number;
}