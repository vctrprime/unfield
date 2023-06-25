using AutoMapper;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.DTO;
using StadiumEngine.DTO.Settings.Breaks;
using StadiumEngine.DTO.Settings.Main;

namespace StadiumEngine.Handlers.Mappings;

public class SettingsProfile : Profile
{
    public SettingsProfile()
    {
        CreateMap<MainSettings, MainSettingsDto>();
        CreateMap<Break, BreakDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>()
            .ForMember( dest => dest.StartHour, 
                act => act.MapFrom( s => TimePointParser.Parse( s.StartHour ) ) )
            .ForMember( dest => dest.EndHour, 
                act => act.MapFrom( s => TimePointParser.Parse( s.EndHour ) ) )
            .ForMember(
                dest => dest.Fields,
                act => act.MapFrom( s => s.BreakFields.Select( bf => bf.Field ) ) );
    }
}