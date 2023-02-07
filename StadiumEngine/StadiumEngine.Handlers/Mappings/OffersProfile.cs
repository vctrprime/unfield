using AutoMapper;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.DTO;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Commands.Offers.LockerRooms;

namespace StadiumEngine.Handlers.Mappings;

internal class OffersProfile : Profile
{
    public OffersProfile()
    {
        CreateMap<LockerRoom, LockerRoomDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>();
        CreateMap<AddLockerRoomCommand, LockerRoom>();
    }
}