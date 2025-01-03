using System.Security.Claims;
using AutoMapper;
using Unfield.Domain.Entities;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Extensions;
using Unfield.DTO;
using Unfield.DTO.Accounts.Permissions;
using Unfield.DTO.Accounts.Roles;
using Unfield.DTO.Accounts.Stadiums;
using Unfield.DTO.Accounts.Users;
using Unfield.Commands.Accounts.Roles;
using Unfield.Commands.Accounts.Users;
using Unfield.Commands.Utils;

namespace Unfield.Handlers.Mappings;

internal class AccountsProfile : Profile
{
    public AccountsProfile()
    {
        CreateMap<User, AuthorizedUserDto>()
            .ForMember(
                dest => dest.UniqueToken,
                act => act.MapFrom( s => s.GetUserToken() ) )
            .ForMember(
                dest => dest.FullName,
                act => act.MapFrom( s => $"{s.Name} {s.LastName}" ) )
            .ForMember( dest => dest.Language, act => act.MapFrom( s => s.Language ?? "ru" ) )
            .ForMember(
                dest => dest.RoleName,
                act => act.MapFrom( s => s.Role == null ? String.Empty : s.Role.Name ) );

        CreateMap<User, AuthorizeUserDto>()
            .IncludeBase<User, AuthorizedUserDto>()
            .ForMember(
                dest => dest.Claims,
                act => act.MapFrom( s => CreateClaimsList( s ) ) );


        CreateMap<Stadium, UserStadiumDto>()
            .ForMember(
                dest => dest.IsCurrent,
                act => act.MapFrom( s => false ) );

        CreateMap<Permission, UserPermissionDto>()
            .ForMember(
                dest => dest.GroupKey,
                act => act.MapFrom( s => s.PermissionGroup.Key ) );

        CreateMap<User, UserDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>()
            .ForMember( dest => dest.RoleId, act => act.MapFrom( s => s.RoleId ) )
            .ForMember(
                dest => dest.RoleName,
                act => act.MapFrom( s => s.Role.Name ) )
            .ForMember( dest => dest.StadiumsCount, act => act.MapFrom( s => s.UserStadiums.Count ) );
        CreateMap<AddUserCommand, User>()
            .ForMember( dest => dest.Language, act => act.MapFrom( s => "ru" ) )
            .ForMember( dest => dest.IsSuperuser, act => act.MapFrom( s => false ) )
            .ForMember( dest => dest.IsAdmin, act => act.MapFrom( s => false ) );
        CreateMap<AddAdminUserCommand, User>()
            .ForMember( dest => dest.Language, act => act.MapFrom( s => "ru" ) )
            .ForMember( dest => dest.IsSuperuser, act => act.MapFrom( s => true ) )
            .ForMember( dest => dest.IsAdmin, act => act.MapFrom( s => true ) );

        CreateMap<Role, RoleDto>()
            .IncludeBase<BaseUserEntity, BaseEntityDto>()
            .ForMember( dest => dest.UsersCount, act => act.MapFrom( s => s.Users.Count ) );
        CreateMap<AddRoleCommand, Role>();

        CreateMap<Permission, PermissionDto>()
            .ForMember( dest => dest.Name, act => act.MapFrom( s => s.Name ) )
            .ForMember( dest => dest.SortValue, act => act.MapFrom( s => s.Sort ) )
            .ForMember( dest => dest.GroupName, act => act.MapFrom( s => s.PermissionGroup.Name ) )
            .ForMember( dest => dest.GroupKey, act => act.MapFrom( s => s.PermissionGroup.Key ) )
            .ForMember( dest => dest.GroupSortValue, act => act.MapFrom( s => s.PermissionGroup.Sort ) );

        CreateMap<Stadium, StadiumDto>()
            .ForMember(
                dest => dest.Country,
                act => act.MapFrom( s => s.City.Region.Country.ShortName ) )
            .ForMember(
                dest => dest.Region,
                act => act.MapFrom( s => s.City.Region.ShortName ) )
            .ForMember(
                dest => dest.City,
                act => act.MapFrom( s => s.City.ShortName ) );
    }

    private List<Claim> CreateClaimsList( User user )
    {
        List<Claim> claims = new()
        {
            new Claim( "id", user.Id.ToString() ),
            new Claim( "stadiumGroupId", user.StadiumGroupId.ToString() ),
            new Claim(
                "stadiumId",
                user.IsSuperuser
                    ? user.StadiumGroup.Stadiums.First().Id.ToString()
                    : user.UserStadiums.First().StadiumId.ToString() )
        };

        return claims;
    }
}