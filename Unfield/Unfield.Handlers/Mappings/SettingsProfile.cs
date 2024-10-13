using AutoMapper;
using Unfield.Commands.Settings.Breaks;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Settings;
using Unfield.DTO;
using Unfield.DTO.Settings.Breaks;
using Unfield.DTO.Settings.Main;

namespace Unfield.Handlers.Mappings;

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