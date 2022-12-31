#nullable enable
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IUserRepository
{
    public Task<User?> Get(string login, string password);
    public Task<User?> Get(int id);
    public Task Update(User user);
}