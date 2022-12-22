using StadiumEngine.Entities.Domain.Offers;

namespace StadiumEngine.Repositories.Abstract.Accounts;

public interface IUserStadiumRepository
{
    public Task<List<Stadium>> Get(int userId);
}