using Microsoft.EntityFrameworkCore;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Data.Infrastructure.Contexts;
using StadiumEngine.Entities.Domain.Accounts;
using StadiumEngine.Repositories.Abstract.Accounts;

namespace StadiumEngine.Repositories.Concrete.Accounts;

internal class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<User?> Get(string login, string password)
    {
        return await Context
            .Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == login && u.Password == password);
    }

    public async Task<User?> Get(int id)
    {
        return await Context
            .Users
            .FindAsync(id);
    }

    public async Task<User> Update(User user)
    {
        var entity = await Context.Users.FindAsync(user.Id);
        
        if (entity == null) throw new DomainException("Пользователь не найден!");
        
        return await base.Update(entity, user);
    }
}