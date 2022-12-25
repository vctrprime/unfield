using StadiumEngine.Entities.Domain.Accounts;

namespace StadiumEngine.Repositories.Abstract.Accounts;

public interface IUserPermissionRepository
{
    public Task<List<Permission>> Get(int userId);
}