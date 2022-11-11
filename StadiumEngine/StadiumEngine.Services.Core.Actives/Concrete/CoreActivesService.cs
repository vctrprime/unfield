using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumEngine.DataAccess.Repositories.Abstract;
using StadiumEngine.Entities;
using StadiumEngine.Services.Core.Actives.Abstract;

namespace StadiumEngine.Services.Core.Actives.Concrete
{
    public class CoreActivesService : ICoreActivesService
    {
        private readonly ITestRepository _repository;

        public CoreActivesService(ITestRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<List<Class1>> Get()
        {
            return await _repository.Get();
        }
    }
}