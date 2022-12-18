using Microsoft.EntityFrameworkCore;
using StadiumEngine.Data.Infrastructure.Contexts;
using StadiumEngine.Entities.Domain.Geo;
using StadiumEngine.Repositories.Abstract;

namespace StadiumEngine.Repositories.Concrete;

internal class TestRepository : ITestRepository
{
    private readonly MainDbContext _context;

    public TestRepository(MainDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<City>> GetAll()
    {
        return await _context.Cities.ToListAsync();
    }

    public async Task<City> Create(City city)
    {
        await _context.Cities.AddAsync(city);
        await _context.SaveChangesAsync();
        
        await _context.Entry(city).Reference(p => p.Region).LoadAsync();

        return city;
    }
}