using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface IUserFacade
{
    Task<string> AddUser(User user, bool isAdminUser = false);
}