using System.Security.Claims;
using AutoMapper;
using StadiumEngine.Domain.Entities;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.DTO;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

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



        CreateMap<User, UserDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>()
            .ForMember(dest => dest.RoleId, act => act.MapFrom(s => s.RoleId))
            .ForMember(dest => dest.RoleName,
                act => act.MapFrom(s => s.Role.Name));
        CreateMap<AddUserCommand, User>()
            .ForMember(dest => dest.IsSuperuser, act => act.MapFrom(s => false));
        
        CreateMap<Role, RoleDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>()
            .ForMember(dest => dest.UsersCount, act => act.MapFrom(s => s.Users.Count))
            .ForMember(dest => dest.StadiumsCount, act => act.MapFrom(s => s.RoleStadiums.Count));
        CreateMap<AddRoleCommand, Role>();
        
        CreateMap<Permission, PermissionDto>()
            .ForMember(dest => dest.Name, act => act.MapFrom(s => s.DisplayName))
            .ForMember(dest => dest.SortValue, act => act.MapFrom(s => s.Sort))
            .ForMember(dest => dest.GroupName, act => act.MapFrom(s => s.PermissionGroup.Name))
            .ForMember(dest => dest.GroupSortValue, act => act.MapFrom(s => s.PermissionGroup.Sort));
        
        CreateMap<Stadium, StadiumDto>()
            .ForMember(dest => dest.Country, 
                act => act.MapFrom(s => s.City.Region.Country.ShortName))
            .ForMember(dest => dest.Region, 
                act => act.MapFrom(s => s.City.Region.ShortName))
            .ForMember(dest => dest.City, 
            act => act.MapFrom(s => s.City.ShortName));

        CreateMap<ToggleRolePermissionCommand, RolePermission>();
        CreateMap<ToggleRoleStadiumCommand, RoleStadium>();

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