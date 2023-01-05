#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IRoleStadiumRepository
{
    Task<RoleStadium?> Get(int roleId, int stadiumId);
    void Add(RoleStadium roleStadium);
    void Remove(RoleStadium roleStadium);
}