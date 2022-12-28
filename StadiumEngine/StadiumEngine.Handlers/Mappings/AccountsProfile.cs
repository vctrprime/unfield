using System.Security.Claims;
using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Entities.Offers;
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
            new (ClaimTypes.Name, $"{user.Name} {user.LastName}"),
            new ("id", user.Id.ToString()),
            new ("legalId", user.LegalId.ToString()),
            new ("isSuperuser", user.IsSuperuser.ToString()),
        };

        if (user.Role == null)
        {
            claims.Add(new Claim("stadiumId", user.Legal.Stadiums.First().Id.ToString()));
            return claims;
        }
        
        
        claims.Add(new Claim("roleName", user.Role.Name));
        claims.Add(new Claim("roleId", user.Role.Id.ToString()));
        claims.Add(new Claim("stadiumId", user.Role.RoleStadiums.First().StadiumId.ToString()));
        claims.AddRange(user.Role.RolePermissions.Select(rp => new Claim("permission", rp.Permission.Name.ToLower())));
        
        return claims;
    }
    
}