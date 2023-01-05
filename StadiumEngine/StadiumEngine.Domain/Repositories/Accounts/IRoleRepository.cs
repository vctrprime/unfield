using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IRoleRepository
{
    Task<List<Role>> GetAll(int legalId);
    void Add(Role role);
    //void Update(Role user);
    //void Remove(Role user);
}