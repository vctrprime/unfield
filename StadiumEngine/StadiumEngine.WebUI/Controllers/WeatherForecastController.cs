using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DataAccess.Repositories.Abstract;
using StadiumEngine.Entities;

namespace StadiumEngine.WebUI.Controllers
{
    [ApiController]
    [Route("test")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ITestRepository _repository;

        public WeatherForecastController(ITestRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<List<Class1>> Get()
        {
            return await _repository.Get();
        }
    }
}