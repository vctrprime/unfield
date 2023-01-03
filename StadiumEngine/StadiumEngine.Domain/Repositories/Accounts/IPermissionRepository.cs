using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IPermissionRepository
{
    Task<List<Permission>> GetAll();
    Task<List<Permission>> GetForRole(int roleId);
}