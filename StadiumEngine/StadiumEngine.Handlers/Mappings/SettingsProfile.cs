using AutoMapper;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.DTO.Settings.Stadiums;

namespace StadiumEngine.Handlers.Mappings;

public class SettingsProfile : Profile
{
    public SettingsProfile()
    {
        CreateMap<StadiumMainSettings, StadiumMainSettingsDto>();
    }
}