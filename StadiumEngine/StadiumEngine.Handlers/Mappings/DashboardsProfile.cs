using AutoMapper;
using Newtonsoft.Json;
using StadiumEngine.Domain.Entities.Dashboard;
using StadiumEngine.DTO.Dashboards;

namespace StadiumEngine.Handlers.Mappings;

internal class DashboardsProfile : Profile
{
    public DashboardsProfile()
    {
        CreateMap<StadiumDashboardYearChartItem, StadiumDashboardChartItemDto>()
            .ForMember(
                dest => dest.Category,
                act => act.MapFrom( s => s.Month ) );
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
            .ForMember(
                dest => dest.TimeChart,
                act => act.MapFrom( s => s.Data.TimeChart.Items ) );
    }
}