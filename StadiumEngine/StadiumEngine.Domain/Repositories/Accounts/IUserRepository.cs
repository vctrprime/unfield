#nullable enable
using System.Threading.Tasks;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Repositories.Accounts;

public interface IUserRepository
{ 
    Task<User?> Get(string login);
    Task<User?> Get(int id);
    void Add(User user);
    void Update(User user);
}