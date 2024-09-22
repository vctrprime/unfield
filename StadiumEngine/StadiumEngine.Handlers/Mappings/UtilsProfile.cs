using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Commands.Utils;
using StadiumEngine.Common.Configuration.Sections;

namespace StadiumEngine.Handlers.Mappings;

internal class UtilsProfile : Profile
{
    public UtilsProfile( EnvConfig envConfig )
    {
        CreateMap<AddStadiumGroupCommand, StadiumGroup>()
            .ForMember(
                dest => dest.Stadiums,
                act => act.MapFrom( s=> MapStadiumGroupStadium( s, envConfig ) ) );

        CreateMap<AddStadiumGroupCommandSuperuser, User>()
            .ForMember( dest => dest.IsSuperuser, act => act.MapFrom( s => true ) )
            .ForMember( dest => dest.IsAdmin, act => act.MapFrom( s => false ) );
        CreateMap<StadiumGroup, AddStadiumGroupDto>();
    }

    private List<Stadium> MapStadiumGroupStadium( AddStadiumGroupCommand source, EnvConfig envConfig )
    {
        List<Stadium> stadiums = source.Stadiums.Select(
            s => new Stadium
            {
                Name = s.Name,
                Address = s.Address,
                Description = s.Description,
                CityId = s.CityId,
                MainSettings = new MainSettings
                {
                    OpenTime = 8,
                    CloseTime = 23
                },
                Token = Guid.NewGuid().ToString(),
                BookingFormHost = envConfig.BookingFormHost
            } ).ToList();

        return stadiums;
    }
}