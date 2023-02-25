using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface ILegalFacade
{
    Task<List<Legal>> GetLegalsByFilter(string searchString);
    Task<string> AddLegal(Legal legal, User superuser);
}