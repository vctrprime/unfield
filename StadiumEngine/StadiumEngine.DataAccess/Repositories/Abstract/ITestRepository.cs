using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.Entities;

namespace StadiumEngine.DataAccess.Repositories.Abstract
{
    public interface ITestRepository
    {
        Task<List<Class1>> Get();
    }
}