using AutoMapper;
using Unfield.Common.Configuration;
using Unfield.Common.Configuration.Sections;
using Unfield.Domain.Entities;
using Unfield.DTO;

namespace Unfield.Handlers.Mappings;

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