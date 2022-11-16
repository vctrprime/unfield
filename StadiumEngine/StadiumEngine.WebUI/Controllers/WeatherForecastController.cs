using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StadiumEngine.DataAccess.Repositories.Abstract;
using StadiumEngine.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace StadiumEngine.WebUI.Controllers
{
    /// <summary>
    /// Тестовый контроллер
    /// </summary>
    [ApiController]
    [Route("test")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ITestRepository _repository;
        private readonly ILogger _logger;
        
        /// <summary>
        /// Тестовый контроллер
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public WeatherForecastController(ITestRepository repository, ILogger<WeatherForecastController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        /// <summary>
        /// Тестовый метод
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status200OK,
            Type = typeof(List<Class1>))]
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
        
        /// <summary>
        /// Тестовый метод с логом ошибки
        /// </summary>
        /// <returns></returns>
        [HttpGet("error")]
        [SwaggerResponse(StatusCodes.Status200OK,
            Type = typeof(List<Class1>))]
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