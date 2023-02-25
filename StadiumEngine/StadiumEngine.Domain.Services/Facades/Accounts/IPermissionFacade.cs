using System.Threading.Tasks;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface IPermissionFacade
{
    Task Sync(IUnitOfWork unitOfWork);
}