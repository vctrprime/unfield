using AutoMapper;
using Mediator;
using Unfield.Domain.Services.Identity;

namespace Unfield.Handlers.Handlers;

internal abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly int _currentStadiumId;
    protected readonly int _stadiumGroupId;

    protected readonly int _userId;
    protected readonly IMapper Mapper;


    protected BaseRequestHandler( IMapper mapper, IClaimsIdentityService? claimsIdentityService = null )
    {
        Mapper = mapper;

        if ( claimsIdentityService == null )
        {
            return;
        }

        _userId = claimsIdentityService.GetUserId();
        _stadiumGroupId = claimsIdentityService.GetStadiumGroupId();
        _currentStadiumId = claimsIdentityService.GetCurrentStadiumId();
    }

    public abstract ValueTask<TResponse> Handle( TRequest request, CancellationToken cancellationToken );
}