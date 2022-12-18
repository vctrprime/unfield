using AutoMapper;
using StadiumEngine.DTO.Test;
using StadiumEngine.Entities.Domain.Geo;
using StadiumEngine.Handlers.Commands.Test;
using StadiumEngine.Handlers.Queries.Test;

namespace StadiumEngine.Handlers.Mappings;

public class TestProfile : Profile
{
    public TestProfile()
    {
        CreateMap<City, TestDto>()
            .ForMember(dest => dest.RegionName, 
                act => act.MapFrom(s => s.Region.Name))
            .ForMember(dest => dest.CountryName, 
                act => act.MapFrom(s => s.Region.Country.Name));

        CreateMap<CreateTestCommand, City>();
    }
}