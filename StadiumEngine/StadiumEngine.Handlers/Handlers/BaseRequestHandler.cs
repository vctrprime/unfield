using AutoMapper;
using StadiumEngine.Services.Auth.Abstract;

namespace StadiumEngine.Handlers.Handlers;

internal abstract class BaseRequestHandler
{
    protected readonly IMapper Mapper;
    protected readonly IClaimsIdentityService ClaimsIdentityService;

    protected BaseRequestHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService)
    {
        Mapper = mapper;
        ClaimsIdentityService = claimsIdentityService;
    }
}