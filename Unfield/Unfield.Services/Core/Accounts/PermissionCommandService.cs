using Unfield.Domain;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Repositories.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Static;

namespace Unfield.Services.Core.Accounts;

internal class PermissionCommandService : IPermissionCommandService
{
    private readonly IPermissionGroupRepository _permissionGroupRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PermissionCommandService( IPermissionGroupRepository permissionGroupRepository, IPermissionRepository permissionRepository, IUnitOfWork unitOfWork )
    {
        _permissionGroupRepository = permissionGroupRepository;
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task SyncPermissionsAsync()
    {
        List<Permission> storedPermissions = await _permissionRepository.GetAllAsync();
        List<PermissionGroup> storedPermissionsGroups = storedPermissions
            .Select( p => p.PermissionGroup )
            .GroupBy( pg => pg.Id )
            .Select( g => g.First() ).ToList();

        foreach ( PermissionGroup? permissionGroup in PermissionSet.PermissionGroups )
        {
            PermissionGroup copyPermissionGroup = new()
            {
                Key = permissionGroup.Key,
                Name = permissionGroup.Name,
                Description = permissionGroup.Description,
                Sort = permissionGroup.Sort
            };

            PermissionGroup? storedPermissionsGroup =
                storedPermissionsGroups.FirstOrDefault( pg => pg.Key == permissionGroup.Key );
            if ( storedPermissionsGroup != null )
            {
                copyPermissionGroup.Id = storedPermissionsGroup.Id;

                storedPermissionsGroup.Name = permissionGroup.Name;
                storedPermissionsGroup.Description = permissionGroup.Description;
                storedPermissionsGroup.Sort = permissionGroup.Sort;

                _permissionGroupRepository.Update( storedPermissionsGroup );
            }
            else
            {
                _permissionGroupRepository.Add( copyPermissionGroup );
            }

            await _unitOfWork.SaveChangesAsync();

            await SyncPermissionsAsync(
                permissionGroup,
                copyPermissionGroup.Id,
                storedPermissions );
        }
    }

    private async Task SyncPermissionsAsync( PermissionGroup permissionGroup, int permissionGroupId,
        List<Permission> storedPermissions )
    {
        foreach ( Permission? permission in permissionGroup.Permissions )
        {
            Permission copyPermission = new()
            {
                PermissionGroupId = permissionGroupId,
                DisplayName = permission.DisplayName,
                Name = permission.Name,
                Description = permission.Description,
                Sort = permission.Sort
            };

            Permission? storedPermission =
                storedPermissions.FirstOrDefault( p => p.Name == copyPermission.Name );
            if ( storedPermission != null )
            {
                storedPermission.PermissionGroupId = permissionGroupId;

                storedPermission.DisplayName = permission.DisplayName;
                storedPermission.Name = permission.Name;
                storedPermission.Description = permission.Description;
                storedPermission.Sort = permission.Sort;

                _permissionRepository.Update( storedPermission );
            }
            else
            {
                _permissionRepository.Add( copyPermission );
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}