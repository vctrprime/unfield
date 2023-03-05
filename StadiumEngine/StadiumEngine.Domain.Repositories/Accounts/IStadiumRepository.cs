using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IStadiumRepository
{
    Task<List<Stadium>> GetForLegal( int legalId );

    Task<List<Stadium>> GetForRole( int roleId );
}