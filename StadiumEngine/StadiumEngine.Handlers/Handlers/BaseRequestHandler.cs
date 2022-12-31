using AutoMapper;
using Mediator;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Handlers.Handlers;

internal abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected readonly IMapper Mapper;
    protected readonly IClaimsIdentityService ClaimsIdentityService;
    protected readonly IUnitOfWork UnitOfWork;

    protected BaseRequestHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork)
    {
        Mapper = mapper;
        ClaimsIdentityService = claimsIdentityService;
        UnitOfWork = unitOfWork;
    }
    public abstract ValueTask<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}