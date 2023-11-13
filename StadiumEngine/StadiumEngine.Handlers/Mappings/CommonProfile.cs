using AutoMapper;
using StadiumEngine.Common.Configuration;
using StadiumEngine.Domain.Entities;
using StadiumEngine.DTO;

namespace StadiumEngine.Handlers.Mappings;

internal class CommonProfile : Profile
{
    public CommonProfile()
    {
        CreateMap<EnvConfig, EnvDataDto>();
        
        CreateMap<BaseUserEntity, BaseEntityDto>()
            .ForMember(
                dest => dest.UserCreated,
                act => act.MapFrom(
                    s => s.UserCreated == null ? "-" : $"{s.UserCreated.Name} {s.UserCreated.LastName}" ) )
            .ForMember(
                dest => dest.UserModified,
                act => act.MapFrom(
                    s => s.UserModified == null ? "-" : $"{s.UserModified.Name} {s.UserModified.LastName}" ) );
    }
}