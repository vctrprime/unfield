using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.DTO.Utils;
using StadiumEngine.Handlers.Commands.Utils;
using StadiumEngine.Handlers.Static;

namespace StadiumEngine.Handlers.Handlers.Utils;

internal sealed class SyncPermissionsHandler : BaseRequestHandler<SyncPermissionsCommand, SyncPermissionsDto>
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPermissionGroupRepository _permissionGroupRepository;
    
    public SyncPermissionsHandler(
        IPermissionRepository permissionRepository,
        IPermissionGroupRepository permissionGroupRepository,
        IMapper mapper, 
        IUnitOfWork unitOfWork) : base(mapper, null, unitOfWork)
    {
        _permissionRepository = permissionRepository;
        _permissionGroupRepository = permissionGroupRepository;
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
                
                _permissionRepository.Update(storedPermission);
            }
            else
            {
                _permissionRepository.Add(copyPermission);
            }
                    
            await UnitOfWork.SaveChanges();
        }
    }
}