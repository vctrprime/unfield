using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.Handlers.Commands.Utils;

namespace StadiumEngine.Handlers.Builders.Utils;

internal class NewLegalBuilder 
{
    private readonly IMapper _mapper;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IHasher _hasher;
    

    public NewLegalBuilder(IMapper mapper, IPermissionRepository permissionRepository,
        IPasswordGenerator passwordGenerator, IHasher hasher)
    {
        _mapper = mapper;
        _permissionRepository = permissionRepository;
        _passwordGenerator = passwordGenerator;
        _hasher = hasher;
    }

    public async Task<(Legal, string)> Build(AddLegalCommand source)
    {
        var legal = _mapper.Map<Legal>(source);
        var stadiums = GetStadiums(source.Stadiums, legal.CityId);
        var (user, password) = GetSuperuser(source.Superuser);
        
        legal.Roles = new List<Role>()
        {
            await GetBaseRole(stadiums)
        };
        legal.Stadiums = stadiums;
        legal.Users = new List<User>
        {
            user
        };

        return (legal, password);
    }
    
    private async Task<Role> GetBaseRole(IEnumerable<Stadium> stadiums)
    {
        var role = new Role
        {
            Name = "Администратор",
            Description = "Базовая роль для администратора (добавлена автоматически)",
            RolePermissions = await GetRolePermissions(),
            RoleStadiums = GetRoleStadiums(stadiums)
            
        };
        
        return role;
    }
    
    private async Task<List<RolePermission>> GetRolePermissions()
    {
        var permissions = await _permissionRepository.GetAll();
        var permissionsKeys = new List<string>
        {
            "schedule", "actives"
        };
            
        var rolePermissions = permissions.Where(p => permissionsKeys.Contains(p.PermissionGroup.Key)).Select(p => new RolePermission()
        {
            Permission = p
        });
        return rolePermissions.ToList();
    }
    
    private List<Stadium> GetStadiums(IEnumerable<AddLegalCommandStadium> requestStadiums, int cityId)
    {
        var stadiums = _mapper.Map<List<Stadium>>(requestStadiums);
        stadiums.ForEach(s =>
        {
            s.CityId = cityId;
        });
        
        return stadiums;
    }
    
    private List<RoleStadium> GetRoleStadiums(IEnumerable<Stadium> stadiums)
    {
        var roleStadiums = stadiums.Select(s => new RoleStadium
        {
            Stadium = s
        });

        return roleStadiums.ToList();
    }
    
    private (User, string) GetSuperuser(AddLegalCommandSuperuser superuser)
    {
        var user = _mapper.Map<User>(superuser);
        
        var superuserPassword = _passwordGenerator.Generate(8);
        user.Password = _hasher.Crypt(superuserPassword);

        return (user, superuserPassword);
    }
}