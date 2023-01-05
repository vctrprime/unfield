using System.Collections.Generic;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IRoleStadiumRepository
{
    void Add(RoleStadium roleStadium);
    //void Remove(int roleId, int stadiumId);
}