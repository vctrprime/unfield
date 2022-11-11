using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using StadiumEngine.DataAccess.Connection.Abstract;
using StadiumEngine.DataAccess.Repositories.Abstract;
using StadiumEngine.Entities;

namespace StadiumEngine.DataAccess.Repositories.Concrete
{
    public class TestRepository : RepositoryBase, ITestRepository
    {
        public TestRepository(IConnectionCreator connectionCreator) : base(connectionCreator)
        {
        }

        public async Task<List<Class1>> Get()
        {
            var data = await ConnectionCreator.Connection.QueryAsync<Class1>("SELECT name FROM public.test");
            
            return data.ToList();
        }
    }
}