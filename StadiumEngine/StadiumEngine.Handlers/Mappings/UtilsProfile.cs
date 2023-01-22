using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;

namespace StadiumEngine.Handlers.Mappings;

internal class UtilsProfile : Profile
{
    public UtilsProfile()
    {
        CreateMap<AddLegalCommand, Legal>();
        CreateMap<AddLegalCommandStadium, Stadium>();
        CreateMap<AddLegalCommandSuperuser, User>()
            .ForMember(dest => dest.IsSuperuser, act => act.MapFrom(s => true))
            .ForMember(dest => dest.IsAdmin, act => act.MapFrom(s => false));
        CreateMap<Legal, AddLegalDto>();
    }
}