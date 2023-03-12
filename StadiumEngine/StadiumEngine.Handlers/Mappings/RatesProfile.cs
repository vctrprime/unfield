using AutoMapper;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.DTO;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Commands.Rates.PriceGroups;
using StadiumEngine.Commands.Rates.Tariffs;

namespace StadiumEngine.Handlers.Mappings;

public class RatesProfile : Profile
{
    public RatesProfile()
    {
        CreateMap<PriceGroup, PriceGroupDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>()
            .ForMember( dest => dest.FieldNames, act => act.MapFrom( s => s.Fields.Select( cf => cf.Name ) ) );
        CreateMap<AddPriceGroupCommand, PriceGroup>();

        CreateMap<Tariff, TariffDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>()
            .ForMember(
                dest => dest.DayIntervals,
                act => act.MapFrom( s => MapDayIntervals( s ) ) );
        CreateMap<AddTariffCommand, Tariff>();
    }

    private static List<string[]> MapDayIntervals( Tariff tariff ) =>
        tariff.TariffDayIntervals
            .Select( x => new[] { x.DayInterval.Start, x.DayInterval.End } ).ToList();
}