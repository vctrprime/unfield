using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;

namespace StadiumEngine.Handlers.Mappings;

internal class UtilsProfile : Profile
{
    public UtilsProfile()
    {
        CreateMap<AddLegalCommand, Legal>()
            .ForMember(dest => dest.Stadiums, act => act.Ignore());
        CreateMap<AddLegalCommandStadium, Stadium>();
        CreateMap<AddLegalCommandSuperuser, User>()
            .ForMember(dest => dest.IsSuperuser, act => act.MapFrom(s => true));
        CreateMap<Legal, AddLegalDto>();
    }
}