using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IUserStadiumRepository
{
    public Task<List<Stadium>> Get(int roleId, int legalId, bool isSuperuser);
}