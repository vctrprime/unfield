using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StadiumEngine.DataAccess.Repositories.Abstract;
using StadiumEngine.Entities;

namespace StadiumEngine.WebUI.Controllers
{
    [ApiController]
    [Route("test")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ITestRepository _repository;
        private readonly ILogger _logger;

        public WeatherForecastController(ITestRepository repository, ILogger<WeatherForecastController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Class1>> Get()
        {
            try
            {
                _logger.LogInformation("terssdfs");
                var result = await _repository.Get();
                return result;
            }
            catch (Exception e)
            {
                return new List<Class1>
                {
                    new Class1
                    {
                        Name = e.Message + ";" + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")+ ";" + Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                    }
                };
            }
        }
        
        [HttpGet("error")]
        public async Task<List<Class1>> GetError()
        {
            try
            {
                _logger.LogError(new Exception("test"), "Тест");
                var result = await _repository.Get();
                return result;
            }
            catch (Exception e)
            {
                return new List<Class1>
                {
                    new Class1
                    {
                        Name = e.Message + ";" + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")+ ";" + Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                    }
                };
            }
        }
    }
}