using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IUserPermissionRepository
{
    public Task<List<Permission>> Get(int userId);
}