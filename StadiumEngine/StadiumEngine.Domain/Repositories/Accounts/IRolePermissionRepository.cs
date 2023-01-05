using System.Collections.Generic;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IRolePermissionRepository
{
    void Add(RolePermission rolePermission);
    //void Remove(int roleId, int permissionId);
}