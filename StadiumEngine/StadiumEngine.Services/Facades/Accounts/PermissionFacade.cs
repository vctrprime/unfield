using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Static;

namespace StadiumEngine.Services.Facades.Accounts;

internal class PermissionFacade : IPermissionFacade
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPermissionGroupRepository _permissionGroupRepository;

    public PermissionFacade(
        IPermissionRepository permissionRepository, 
        IPermissionGroupRepository permissionGroupRepository)
    {
        _permissionRepository = permissionRepository;
        _permissionGroupRepository = permissionGroupRepository;
    }


    public async Task Sync(IUnitOfWork unitOfWork)
    {
        await SyncPermissionsAndGroups(unitOfWork);
    }
    
    private async Task SyncPermissionsAndGroups(IUnitOfWork unitOfWork)
    {
        var storedPermissions = await _permissionRepository.GetAll();
        var storedPermissionsGroups = storedPermissions
            .Select(p => p.PermissionGroup)
            .GroupBy(pg => pg.Id)
            .Select(g => g.First()).ToList();
        
        foreach (var permissionGroup in PermissionSet.PermissionGroups)
        {
            var copyPermissionGroup = new PermissionGroup
            {
                Key = permissionGroup.Key,
                Name = permissionGroup.Name,
                Description = permissionGroup.Description,
                Sort = permissionGroup.Sort
            };
            
            var storedPermissionsGroup =
                storedPermissionsGroups.FirstOrDefault(pg => pg.Key == permissionGroup.Key);
            if (storedPermissionsGroup != null)
            {
                copyPermissionGroup.Id = storedPermissionsGroup.Id;
                
                storedPermissionsGroup.Name = permissionGroup.Name;
                storedPermissionsGroup.Description = permissionGroup.Description;
                storedPermissionsGroup.Sort = permissionGroup.Sort;
                
                _permissionGroupRepository.Update(storedPermissionsGroup);
            }
            else
            {
                _permissionGroupRepository.Add(copyPermissionGroup);
            }

            await unitOfWork.SaveChanges();

            await SyncPermissions(unitOfWork, permissionGroup, copyPermissionGroup.Id, storedPermissions);
        }
    }

    private async Task SyncPermissions(IUnitOfWork unitOfWork, PermissionGroup permissionGroup, int permissionGroupId, List<Permission> storedPermissions)
    {
        foreach (var permission in permissionGroup.Permissions)
        {
            var copyPermission = new Permission()
            {
                PermissionGroupId = permissionGroupId,
                DisplayName = permission.DisplayName,
                Name = permission.Name,
                Description = permission.Description,
                Sort = permission.Sort
                
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
                
                _permissionRepository.Update(storedPermission);
            }
            else
            {
                _permissionRepository.Add(copyPermission);
            }
                    
            await unitOfWork.SaveChanges();
        }
    }
}