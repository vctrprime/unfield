using System.Security.Claims;
using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Mappings;

internal class AccountsProfile : Profile
{
    public AccountsProfile()
    {
        CreateMap<User, AuthorizeUserDto>()
            .ForMember(dest => dest.FullName, 
                act => act.MapFrom(s => $"{s.Name} {s.LastName}"))
            .ForMember(dest => dest.RoleName, act => act.MapFrom(s => s.Role == null ? null : s.Role.Name))
            .ForMember(dest => dest.Claims,
                act => act.MapFrom(s => CreateClaimsList(s)));

        CreateMap<User, AuthorizedUserDto>()
            .ForMember(dest => dest.FullName,
                act => act.MapFrom(s => $"{s.Name} {s.LastName}"))
            .ForMember(dest => dest.RoleName,
                act => act.MapFrom(s => s.Role == null ? "Суперпользователь" : s.Role.Name));
        
        CreateMap<Stadium, UserStadiumDto>()
            .ForMember(dest => dest.IsCurrent, 
                act => act.MapFrom(s => false));
        
        CreateMap<Permission, UserPermissionDto>()
            .ForMember(dest => dest.GroupKey, 
                act => act.MapFrom(s => s.PermissionGroup.Key));
    }
    
    private List<Claim> CreateClaimsList(User user)
    {
        var claims = new List<Claim>
        {
            new ("id", user.Id.ToString()),
            new ("legalId", user.LegalId.ToString()),
            new ("stadiumId", user.Role == null ? 
                user.Legal.Stadiums.First().Id.ToString() : 
                user.Role.RoleStadiums.First().StadiumId.ToString())
        };
        
        return claims;
    }
    
}