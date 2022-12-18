using StadiumEngine.Entities.Domain.Geo;

namespace StadiumEngine.Repositories.Abstract;

public interface ITestRepository
{
    public Task<List<City>> GetAll();
    public Task<City> Create(City city);
}