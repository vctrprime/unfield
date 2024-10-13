using AutoMapper;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Rates;
using Unfield.DTO;
using Unfield.DTO.Rates.PriceGroups;
using Unfield.DTO.Rates.Tariffs;
using Unfield.Commands.Rates.PriceGroups;
using Unfield.Commands.Rates.Tariffs;
using Unfield.Common.Enums.Offers;
using Unfield.DTO.Rates.Prices;

namespace Unfield.Handlers.Mappings;

public class RatesProfile : Profile
{
    public RatesProfile()
    {
        CreateMap<PriceGroup, PriceGroupDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>()
            .ForMember( dest => dest.FieldNames, act => act.MapFrom( s => s.Fields.Select( cf => cf.Name ) ) );
        CreateMap<AddPriceGroupCommand, PriceGroup>();

        CreateMap<PromoCode, PromoCodeDto>();
        CreateMap<PromoCodeDto, PromoCode>();
        CreateMap<Tariff, TariffDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>()
            .ForMember(
                dest => dest.DayIntervals,
                act => act.MapFrom( s => MapDayIntervals( s ) ) );
        CreateMap<AddTariffCommand, Tariff>();
        

        CreateMap<Price, PriceDto>();
        CreateMap<PriceDto, Price>()
            .ForMember( dest => dest.Currency, act => act.MapFrom( s => Currency.Rub ) );
    }

    private static List<TariffDayIntervalDto> MapDayIntervals( Tariff tariff )
    {
        if ( tariff.TariffDayIntervals == null )
        {
            return new List<TariffDayIntervalDto>();
        }
        
        return tariff.TariffDayIntervals
            .Select( x => new TariffDayIntervalDto
            {
                TariffDayIntervalId = x.Id,
                Interval = new[] { x.DayInterval.Start, x.DayInterval.End }
            }).ToList();
    }
        
}