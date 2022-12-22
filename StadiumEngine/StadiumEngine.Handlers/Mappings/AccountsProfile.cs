using System.Security.Claims;
using AutoMapper;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Entities.Domain.Accounts;
using StadiumEngine.Entities.Domain.Offers;

namespace StadiumEngine.Handlers.Mappings;

internal class AccountsProfile : Profile
{
    public AccountsProfile()
    {
        CreateMap<User, AuthorizeUserDto>()
            .ForMember(dest => dest.FullName, 
                act => act.MapFrom(s => $"{s.Name} {s.LastName}"))
            .ForMember(dest => dest.Claims,
                act => act.MapFrom(s => CreateClaimsList(s)));

        CreateMap<Stadium, UserStadiumDto>()
            .ForMember(dest => dest.IsCurrent, 
                act => act.MapFrom(s => false));
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
        
        
        claims.Add(new Claim("roleId", user.Role.Id.ToString()));
        claims.Add(new Claim("stadiumId", user.Role.RoleStadiums.First().StadiumId.ToString()));
        claims.AddRange(user.Role.RolePermissions.Select(rp => new Claim("permission", rp.Permission.Name.ToLower())));
        
        return claims;
    }
    
}