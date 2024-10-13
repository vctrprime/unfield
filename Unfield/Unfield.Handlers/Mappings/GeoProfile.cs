using AutoMapper;
using Unfield.Domain.Entities.Geo;
using Unfield.DTO.Geo;

namespace Unfield.Handlers.Mappings;

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