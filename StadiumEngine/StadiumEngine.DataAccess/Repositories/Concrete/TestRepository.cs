using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StadiumEngine.DataAccess.Contexts;
using StadiumEngine.DataAccess.Repositories.Abstract;
using StadiumEngine.Entities.Domain.Geo;

namespace StadiumEngine.DataAccess.Repositories.Concrete
{
    public class TestRepository : ITestRepository
    {
        private readonly GeoDbContext _context;

        public TestRepository(GeoDbContext context)
        {
            _context = context;
        }

        public async Task<List<City>> Get()
        {
            var data = await _context.Cities.ToListAsync();
            
            return data;
        }
    }
}