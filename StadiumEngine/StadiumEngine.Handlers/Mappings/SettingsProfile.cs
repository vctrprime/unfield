using AutoMapper;
using StadiumEngine.Commands.Settings.Breaks;
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
            .ForMember(
                dest => dest.SelectedFields,
                act => act.MapFrom( s => s.BreakFields.Select( bf => bf.FieldId ) ) );
        CreateMap<AddBreakCommand, Break>()
            .ForMember(
                dest => dest.BreakFields,
                act => act.MapFrom(
                    s => s.SelectedFields.Select(
                        x => new BreakField
                        {
                            FieldId = x
                        } ) ) );
    }
}