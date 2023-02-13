using AutoMapper;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.DTO;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Commands.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Mappings;

internal class OffersProfile : Profile
{
    public OffersProfile()
    {
        CreateMap<LockerRoom, LockerRoomDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>();
        CreateMap<AddLockerRoomCommand, LockerRoom>();

        CreateMap<Field, FieldDto>()
            .ForMember(dest => dest.SportKinds, act => act.MapFrom(s => s.FieldSportKinds.Select(k => k.SportKind).ToList()))
            .ForMember(dest => dest.Images, act => act.MapFrom(s => s.Images.OrderBy(i => i.Order).Select(i => i.Path).ToList()));
        CreateMap<AddFieldCommand, Field>()
            .ForMember(dest => dest.FieldSportKinds, act => act.MapFrom(s => s.SportKinds.Select(k => new FieldSportKind
            {
                SportKind = k
            })))
            .ForMember(dest => dest.Images, act => act.Ignore());
    }
}