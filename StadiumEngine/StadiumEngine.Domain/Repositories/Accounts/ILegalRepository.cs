using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface ILegalRepository
{
    Task<List<Legal>> GetByFilter(string searchString);
    void Add(Legal legal);
}