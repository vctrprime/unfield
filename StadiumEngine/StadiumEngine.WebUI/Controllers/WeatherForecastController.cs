using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.Entities;
using StadiumEngine.Services.Core.Actives.Abstract;

namespace StadiumEngine.WebUI.Controllers
{
    [ApiController]
    [Route("test")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ICoreActivesService _coreActivesService;

        public WeatherForecastController(ICoreActivesService coreActivesService)
        {
            _coreActivesService = coreActivesService;
        }

        [HttpGet]
        public async Task<List<Class1>> Get()
        {
            return await _coreActivesService.Get();
        }
    }
}