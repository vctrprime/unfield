using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StadiumEngine.DTO.Test;
using StadiumEngine.Handlers.Commands.Test;
using StadiumEngine.Handlers.Queries.Test;

namespace StadiumEngine.WebUI.Controllers.API;

[Route("api/[controller]")]
public class TestController : BaseApiController
{
    [HttpGet]
    public async Task<List<TestDto>> GetAll()
    {
        var result = await Mediator.Send(new GetAllTestQuery());
        return result;
    }
    
    [HttpPost]
    public async Task<TestDto> Create(CreateTestCommand command)
    {
        var result = await Mediator.Send(command);
        return result;
    }
}