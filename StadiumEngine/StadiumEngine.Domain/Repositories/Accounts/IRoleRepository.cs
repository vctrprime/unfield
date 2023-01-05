#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IRoleRepository
{
    Task<List<Role>> GetAll(int legalId);
    Task<Role?> Get(int roleId);
    void Add(Role role);
    //void Update(Role user);
    //void Remove(Role user);
}