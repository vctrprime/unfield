using AutoMapper;
using Mediator;
using Unfield.Domain;
using Unfield.Domain.Services.Identity;

namespace Unfield.Handlers.Handlers;

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
            return await HandleCommandAsync( request, cancellationToken );
        }

        try
        {
            await UnitOfWork.BeginTransactionAsync();

            TResponse result = await HandleCommandAsync( request, cancellationToken );

            await UnitOfWork.CommitTransactionAsync();
            
            return result;
        }
        catch( Exception ex )
        {
            await UnitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    protected abstract ValueTask<TResponse> HandleCommandAsync( TRequest request, CancellationToken cancellationToken );
}