using AutoMapper;
using Mediator;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Handlers.Handlers;

internal abstract class BaseCommandHandler<TRequest, TResponse> : BaseRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly bool _transactional;
    protected readonly IUnitOfWork UnitOfWork;

    protected BaseCommandHandler(
        IMapper mapper,
        IClaimsIdentityService? claimsIdentityService,
        IUnitOfWork unitOfWork,
        bool transactional = true ) : base( mapper, claimsIdentityService )
    {
        UnitOfWork = unitOfWork;
        _transactional = transactional;
    }

    public override async ValueTask<TResponse> Handle( TRequest request, CancellationToken cancellationToken )
    {
        if ( !_transactional )
        {
            return await HandleCommand( request, cancellationToken );
        }

        await UnitOfWork.BeginTransaction();

        TResponse result = await HandleCommand( request, cancellationToken );

        await UnitOfWork.CommitTransaction();

        return result;
    }

    protected abstract ValueTask<TResponse> HandleCommand( TRequest request, CancellationToken cancellationToken );
}