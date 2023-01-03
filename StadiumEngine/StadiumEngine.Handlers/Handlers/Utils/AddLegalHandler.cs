using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class AddLegalHandler : BaseRequestHandler<AddLegalCommand, AddLegalDto>
{
    private readonly ILegalRepository _legalRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IStadiumRepository _stadiumRepository;
    private readonly IRoleStadiumRepository _roleStadiumRepository;
    private readonly IUserRepository _userRepository;
    

    public AddLegalHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, 
        ILegalRepository legalRepository,
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IRolePermissionRepository rolePermissionRepository,
        IStadiumRepository stadiumRepository,
        IRoleStadiumRepository roleStadiumRepository,
        IUserRepository userRepository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _legalRepository = legalRepository;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _stadiumRepository = stadiumRepository;
        _roleStadiumRepository = roleStadiumRepository;
        _userRepository = userRepository;
    }

    public override async ValueTask<AddLegalDto> Handle(AddLegalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.BeginTransaction();
            
            //добавляем легал
            var legal = await AddLegal(request);
            //добавляем роль Администратор
            var role = await AddBaseRole(legal.Id);
            //добавляем связь пермишеннов и ролей
            await AddRolePermissions(role.Id);
            //добавляем стадионы
            var stadiums = await AddStadiums(request, legal.Id, legal.CityId);
            //добавляем связь стадинов и роли
            await AddRoleStadiums(stadiums, role.Id);
            //добавляем суперюзера
            await AddSuperuser(request, legal.Id);
            
            await UnitOfWork.CommitTransaction();

            var legalDto = Mapper.Map<AddLegalDto>(legal);
            return legalDto;
        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }
    }

    private async Task<Legal> AddLegal(AddLegalCommand request)
    {
        var legal = Mapper.Map<Legal>(request);
        _legalRepository.Add(legal);
        await UnitOfWork.SaveChanges();

        return legal;
    }

    private async Task<Role> AddBaseRole(int legalId)
    {
        var role = new Role
        {
            Name = "Администратор",
            Description = "Базовая роль для администратора (добавлена автоматически)",
            LegalId = legalId,
        };
        _roleRepository.Add(role);
        await UnitOfWork.SaveChanges();

        return role;
    }

    private async Task AddRolePermissions(int roleId)
    {
        var permissions = await _permissionRepository.GetAll();
        var permissionsKeys = new List<string>
        {
            "schedule", "actives"
        };
            
        var rolePermissions = permissions.Where(p => permissionsKeys.Contains(p.PermissionGroup.Key)).Select(p => new RolePermission()
        {
            RoleId = roleId,
            PermissionId = p.Id
        });
        _rolePermissionRepository.Add(rolePermissions);
        await UnitOfWork.SaveChanges();
    }

    private async Task<List<Stadium>> AddStadiums(AddLegalCommand request, int legalId, int cityId)
    {
        var stadiums = Mapper.Map<List<Stadium>>(request.Stadiums);
        stadiums.ForEach(s =>
        {
            s.LegalId = legalId;
            s.CityId = cityId;
        });
        _stadiumRepository.Add(stadiums);
        await UnitOfWork.SaveChanges();

        return stadiums;
    }

    private async Task AddRoleStadiums(List<Stadium> stadiums, int roleId)
    {
        var roleStadiums = stadiums.Select(s => new RoleStadium
        {
            StadiumId = s.Id,
            RoleId = roleId
        });
        _roleStadiumRepository.Add(roleStadiums);
        await UnitOfWork.SaveChanges();

    }

    private async Task AddSuperuser(AddLegalCommand request, int legalId)
    {
        var user = Mapper.Map<User>(request.Superuser);
            
        //toDo генерация пароля
        user.Password = "123456";
        user.LegalId = legalId;
            
        _userRepository.Add(user);
        await UnitOfWork.SaveChanges();
    }
}