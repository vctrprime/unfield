using AutoMapper;
using Mediator;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Handlers.Handlers;

internal abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected readonly IMapper Mapper;
    protected readonly IClaimsIdentityService ClaimsIdentityService;

    protected BaseRequestHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService)
    {
        Mapper = mapper;
        ClaimsIdentityService = claimsIdentityService;
    }
    public abstract ValueTask<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}