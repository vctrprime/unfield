using AutoMapper;
using StadiumEngine.Domain.Entities.Geo;
using StadiumEngine.DTO.Geo;

namespace StadiumEngine.Handlers.Mappings;

public class GeoProfile : Profile
{
    public GeoProfile()
    {
        CreateMap<City, CityDto>()
            .ForMember(
                dest => dest.CountryName,
                act => act.MapFrom( s => s.Region.Country.Name ) )
            .ForMember(
                dest => dest.CountryShortName,
                act => act.MapFrom( s => s.Region.Country.ShortName ) );
    }
}