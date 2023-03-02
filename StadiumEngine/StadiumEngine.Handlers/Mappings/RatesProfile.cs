using AutoMapper;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.DTO;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Handlers.Commands.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Mappings;

public class RatesProfile : Profile
{
    public RatesProfile()
    {
        CreateMap<PriceGroup, PriceGroupDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>()
            .ForMember(dest => dest.FieldNames, act => act.MapFrom(s => s.Fields.Select(cf => cf.Name)));
        CreateMap<AddPriceGroupCommand, PriceGroup>();
    }
}