using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Entities;
using StadiumEngine.Entities.Domain.Geo;

namespace StadiumEngine.DataAccess.Repositories.Abstract
{
    public interface ITestRepository
    {
        Task<List<City>> Get();
    }
}