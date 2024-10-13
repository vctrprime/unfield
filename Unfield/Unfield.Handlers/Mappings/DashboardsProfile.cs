using AutoMapper;
using Unfield.Domain.Entities.Dashboard;
using Unfield.DTO.Dashboards;

namespace Unfield.Handlers.Mappings;

internal class DashboardsProfile : Profile
{
    public DashboardsProfile()
    {
        CreateMap<StadiumDashboardYearChartItem, StadiumDashboardChartItemDto>()
            .ForMember(
                dest => dest.Category,
                act => act.MapFrom( s => s.Month ) );
        CreateMap<StadiumDashboardAverageBill, StadiumDashboardAverageBillDto>();
        CreateMap<StadiumDashboardFieldDistributionItem, StadiumDashboardChartItemDto>()
            .ForMember(
                dest => dest.Category,
                act => act.MapFrom( s => s.Field ) );
        CreateMap<StadiumDashboardPopularInventoryItem, StadiumDashboardChartItemDto>()
            .ForMember(
                dest => dest.Category,
                act => act.MapFrom( s => s.Inventory ) );
        CreateMap<StadiumDashboardTimeChartItem, StadiumDashboardChartItemDto>()
            .ForMember(
                dest => dest.Category,
                act => act.MapFrom( s => s.Time ) );
        
        CreateMap<StadiumDashboard, StadiumDashboardDto>()
            .ForMember( dest => dest.CalculationDate, act => 
                act.MapFrom( s => s.DateCreated ) )
            .ForMember(
                dest => dest.YearChart,
                act => act.MapFrom( s => s.Data.YearChart.Items ) )
            .ForMember( dest => dest.AverageBill, 
                act => act.MapFrom( s => s.Data.AverageBill ) )
            .ForMember(
                dest => dest.FieldDistribution,
                act => act.MapFrom( s => s.Data.FieldDistribution.Items.Take( 8 ) ) )
            .ForMember(
                dest => dest.PopularInventory,
                act => act.MapFrom( s => s.Data.PopularInventory.Items.OrderBy( x => x.Value ).Take( 5 ) ) )
            .ForMember(
                dest => dest.TimeChart,
                act => act.MapFrom( s => s.Data.TimeChart.Items ) );
    }
}