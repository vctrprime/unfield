using StadiumEngine.Entities.Domain.Accounts;

namespace StadiumEngine.Repositories.Abstract.Accounts;

public interface IUserRepository
{
    public Task<User?> Get(string login, string password);
    public Task<User?> Get(int id);
    public Task<User> Update(User user);
}