export interface StadiumDashboardDto {
    calculationDate?: Date;
    yearChart: StadiumDashboardChartItemDto[];
    timeChart: StadiumDashboardChartItemDto[];
}

export interface StadiumDashboardChartItemDto {
    category: string;
    value: number;
}