using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.DTO.Admin;

namespace StadiumEngine.Handlers.Mappings;

internal class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<Legal, LegalDto>()
            .ForMember(dest => dest.City,
                act => act.MapFrom(s => s.City.ShortName))
            .ForMember(dest => dest.Region,
                act => act.MapFrom(s => s.City.Region.ShortName))
            .ForMember(dest => dest.Country,
                act => act.MapFrom(s => s.City.Region.Country.ShortName));

    }
}