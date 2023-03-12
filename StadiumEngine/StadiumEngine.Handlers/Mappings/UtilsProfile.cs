using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Commands.Utils;

namespace StadiumEngine.Handlers.Mappings;

internal class UtilsProfile : Profile
{
    public UtilsProfile()
    {
        CreateMap<AddLegalCommand, Legal>()
            .ForMember(
                dest => dest.Stadiums,
                act => act.MapFrom( s => MapLegalStadium( s ) ) );

        CreateMap<AddLegalCommandSuperuser, User>()
            .ForMember( dest => dest.IsSuperuser, act => act.MapFrom( s => true ) )
            .ForMember( dest => dest.IsAdmin, act => act.MapFrom( s => false ) );
        CreateMap<Legal, AddLegalDto>();
    }

    private List<Stadium> MapLegalStadium( AddLegalCommand source )
    {
        List<Stadium> stadiums = source.Stadiums.Select(
            s => new Stadium
            {
                Name = s.Name,
                Address = s.Address,
                Description = s.Description,
                CityId = source.CityId,
                MainSettings = new StadiumMainSettings
                {
                    OpenTime = 8,
                    CloseTime = 23
                }
            } ).ToList();

        return stadiums;
    }
}