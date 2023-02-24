using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface ILegalFacade
{
    Task<string> AddLegal(Legal legal, User superuser);
}