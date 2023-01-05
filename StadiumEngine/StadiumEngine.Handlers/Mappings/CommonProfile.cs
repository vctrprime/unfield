using AutoMapper;
using StadiumEngine.Domain.Entities;
using StadiumEngine.DTO;

namespace StadiumEngine.Handlers.Mappings;

internal class CommonProfile : Profile
{
    public CommonProfile()
    {
        CreateMap<BaseUserEntity, BaseEntityDto>()
            .ForMember(dest => dest.UserCreatedName,
                act => act.MapFrom(s => s.UserCreated == null ? "-" : $"{s.UserCreated.Name} {s.UserCreated.LastName}"))
            .ForMember(dest => dest.UserModifiedName,
            act => act.MapFrom(s => s.UserModified == null ? "-" : $"{s.UserModified.Name} {s.UserModified.LastName}"));
    }
}