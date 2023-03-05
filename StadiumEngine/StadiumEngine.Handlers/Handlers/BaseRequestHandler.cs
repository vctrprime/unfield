using AutoMapper;
using Mediator;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Handlers.Handlers;

internal abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly int _currentStadiumId;
    protected readonly int _legalId;

    protected readonly int _userId;
    protected readonly IMapper Mapper;


    protected BaseRequestHandler( IMapper mapper, IClaimsIdentityService? claimsIdentityService )
    {
        Mapper = mapper;

        if ( claimsIdentityService != null )
        {
            _userId = claimsIdentityService.GetUserId();
            _legalId = claimsIdentityService.GetLegalId();
            _currentStadiumId = claimsIdentityService.GetCurrentStadiumId();
        }
    }

    public abstract ValueTask<TResponse> Handle( TRequest request, CancellationToken cancellationToken );
}