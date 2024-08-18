using AutoMapper;
using Newtonsoft.Json;
using StadiumEngine.Domain.Entities.Dashboard;
using StadiumEngine.DTO.Dashboards;

namespace StadiumEngine.Handlers.Mappings;

internal class DashboardsProfile : Profile
{
    public DashboardsProfile()
    {
        // todo сделать нормальный объект под нужны фронта и маппинг
        CreateMap<StadiumDashboard, StadiumDashboardDto>()
            .ForMember(
                dest => dest.Test,
                act => act.MapFrom( s => JsonConvert.SerializeObject( s ) ) );
    }
}