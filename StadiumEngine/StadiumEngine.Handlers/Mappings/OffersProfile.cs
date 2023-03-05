using AutoMapper;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.DTO;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.Handlers.Commands.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers.Inventories;
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
            .IncludeBase<BaseUserEntity, BaseEntityDto>()
            .ForMember( dest => dest.PriceGroupId, act => act.MapFrom( s => MapPriceGroupId( s.PriceGroup ) ) )
            .ForMember( dest => dest.PriceGroupName, act => act.MapFrom( s => MapPriceGroupName( s.PriceGroup ) ) )
            .ForMember(
                dest => dest.SportKinds,
                act => act.MapFrom( s => s.SportKinds.Select( k => k.SportKind ).ToList() ) )
            .ForMember(
                dest => dest.Images,
                act => act.MapFrom( s => s.Images.OrderBy( i => i.Order ).Select( i => i.Path ).ToList() ) )
            .ForMember( dest => dest.ChildNames, act => act.MapFrom( s => s.ChildFields.Select( cf => cf.Name ) ) );

        CreateMap<AddFieldCommand, Field>()
            .ForMember(
                dest => dest.SportKinds,
                act => act.MapFrom(
                    s => s.SportKinds.Select(
                        k => new OffersSportKind { SportKind = k } ) ) )
            .ForMember( dest => dest.Images, act => act.Ignore() );

        CreateMap<Inventory, InventoryDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>()
            .ForMember(
                dest => dest.SportKinds,
                act => act.MapFrom( s => s.SportKinds.Select( k => k.SportKind ).ToList() ) )
            .ForMember(
                dest => dest.Images,
                act => act.MapFrom( s => s.Images.OrderBy( i => i.Order ).Select( i => i.Path ).ToList() ) );

        CreateMap<AddInventoryCommand, Inventory>()
            .ForMember(
                dest => dest.SportKinds,
                act => act.MapFrom(
                    s => s.SportKinds.Select(
                        k => new OffersSportKind { SportKind = k } ) ) )
            .ForMember( dest => dest.Images, act => act.Ignore() );
    }

    private int? MapPriceGroupId( PriceGroup? priceGroup ) => priceGroup?.Id;

    private string? MapPriceGroupName( PriceGroup? priceGroup ) => priceGroup?.Name;
}