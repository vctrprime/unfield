using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;
using StadiumEngine.Handlers.Containers.Utils;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class SyncPermissionsHandler : BaseRequestHandler<SyncPermissionsCommand, SyncPermissionsDto>
{
    private readonly SyncPermissionsHandlerRepositoriesContainer _repositoriesContainer;

    private List<PermissionGroup> _permissionGroups = new List<PermissionGroup>
    {
        new PermissionGroup
        {
            Key = "schedule",
            Sort = 2,
            Name = "Шахматка броней",
            Permissions = new List<Permission>
            {
                new() { Name = "get-bookings", DisplayName = "Просмотр броней", Sort = 1}
            }
        },
        new PermissionGroup
        {
            Key = "actives",
            Sort = 3,
            Name = "Активы",
            Permissions = new List<Permission>
            {
                new() { Name = "get-fields", DisplayName = "Просмотр площадок", Sort = 1}
            }
        },
        new PermissionGroup
        {
            Key = "employees",
            Sort = 4,
            Name = "Персонал",
            Permissions = new List<Permission>
            {
                new() { Name = "get-employees", DisplayName = "Просмотр сотрудников", Sort = 1}
            }
        },
        new PermissionGroup
        {
            Key = "reports",
            Sort = 5,
            Name = "Отчетность",
            Permissions = new List<Permission>
            {
                new() { Name = "get-reports", DisplayName = "Просмотр отчетов", Sort = 1}
            }
        },
        new PermissionGroup
        {
            Key = "accounts",
            Sort = 6,
            Name = "Аккаунты и роли",
            Description = "Управление доступом к приложению",
            Permissions = new List<Permission>
            {
                new() { Name = "get-users", DisplayName = "Просмотр пользователей", Sort = 1},
                new() { Name = "get-roles", DisplayName = "Просмотр ролей", Sort = 5},
                new() { Name = "insert-user", DisplayName = "Добавление пользователя", Sort = 2},
                new() { Name = "insert-role", DisplayName = "Добавление роли", Sort = 6},
                new() { Name = "update-user", DisplayName = "Обновление пользователя", Sort = 3},
                new() { Name = "update-role", DisplayName = "Обновление роли", Sort = 7},
                new() { Name = "delete-user", DisplayName = "Удаление пользователя", Sort = 4},
                new() { Name = "delete-role", DisplayName = "Удаление роли", Sort = 8},
            }
        },
        new PermissionGroup
        {
            Key = "main-settings",
            Sort = 1,
            Name = "Основные настройки",
            Description = "Управление основными настройками объекта",
            Permissions = new List<Permission>
            {
                new() { Name = "update-main-settings", DisplayName = "Обновление основных настроек", Sort = 1},
            }
        },
    };
    
    public SyncPermissionsHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork, SyncPermissionsHandlerRepositoriesContainer repositoriesContainer) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repositoriesContainer = repositoriesContainer;
    }

    public override async ValueTask<SyncPermissionsDto> Handle(SyncPermissionsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.BeginTransaction();
            
            await SyncPermissionsAndGroups();
            
            await UnitOfWork.CommitTransaction();
            
            return new SyncPermissionsDto();
        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }
    }

    private async Task SyncPermissionsAndGroups()
    {
        var storedPermissions = await _repositoriesContainer.PermissionRepository.GetAll();
        var storedPermissionsGroups = storedPermissions
            .Select(p => p.PermissionGroup)
            .GroupBy(pg => pg.Id)
            .Select(g => g.First()).ToList();
        
        foreach (var permissionGroup in _permissionGroups)
        {
            var copyPermissionGroup = new PermissionGroup
            {
                Key = permissionGroup.Key,
                Name = permissionGroup.Name,
                Description = permissionGroup.Description
            };
            
            var storedPermissionsGroup =
                storedPermissionsGroups.FirstOrDefault(pg => pg.Key == permissionGroup.Key);
            if (storedPermissionsGroup != null)
            {
                copyPermissionGroup.Id = storedPermissionsGroup.Id;
                
                storedPermissionsGroup.Name = permissionGroup.Name;
                storedPermissionsGroup.Description = permissionGroup.Description;
                storedPermissionsGroup.Sort = permissionGroup.Sort;
                
                _repositoriesContainer.PermissionGroupRepository.Update(storedPermissionsGroup);
            }
            else
            {
                _repositoriesContainer.PermissionGroupRepository.Add(copyPermissionGroup);
            }

            await UnitOfWork.SaveChanges();

            await SyncPermissions(permissionGroup, copyPermissionGroup.Id, storedPermissions);
        }
    }

    private async Task SyncPermissions(PermissionGroup permissionGroup, int permissionGroupId, List<Permission> storedPermissions)
    {
        foreach (var permission in permissionGroup.Permissions)
        {
            var copyPermission = new Permission()
            {
                PermissionGroupId = permissionGroupId,
                DisplayName = permission.DisplayName,
                Name = permission.Name,
                Description = permission.Description
            };
                    
            var storedPermission =
                storedPermissions.FirstOrDefault(p => p.Name == copyPermission.Name);
            if (storedPermission != null)
            {
                storedPermission.PermissionGroupId = permissionGroupId;
                
                storedPermission.DisplayName = permission.DisplayName;
                storedPermission.Name = permission.Name;
                storedPermission.Description = permission.Description;
                storedPermission.Sort = permission.Sort;
                
                _repositoriesContainer.PermissionRepository.Update(storedPermission);
            }
            else
            {
                _repositoriesContainer.PermissionRepository.Add(copyPermission);
            }
                    
            await UnitOfWork.SaveChanges();
        }
    }


}