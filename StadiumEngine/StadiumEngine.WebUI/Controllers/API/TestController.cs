using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DataAccess.Repositories.Abstract;

namespace StadiumEngine.WebUI.Controllers.API;

[Route("api/test")]
[ApiController]
public class TestController
{
    private readonly ITestRepository _repository;

    public TestController(ITestRepository repository)
    {
        _repository = repository;
    }
    public async Task<bool> Get()
    {
        var a = await _repository.Get();

        var region = a.First().Region;
        var regionName = region.Name;
        
        var country = region.Country.Name;
        
        return true;
    }
    
}