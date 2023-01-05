using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;
using StadiumEngine.Handlers.Containers.Utils;
using StadiumEngine.Handlers.Static;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class SyncPermissionsHandler : BaseRequestHandler<SyncPermissionsCommand, SyncPermissionsDto>
{
    private readonly SyncPermissionsHandlerRepositoriesContainer _repositoriesContainer;

    
    
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