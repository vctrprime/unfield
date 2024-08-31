using AutoMapper;
using Mediator;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Handlers.Handlers;

internal abstract class BaseCustomerCommandHandler<TRequest, TResponse> : BaseCustomerRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly bool _transactional;
    protected readonly IUnitOfWork UnitOfWork;

    protected BaseCustomerCommandHandler(
        IMapper mapper,
        ICustomerClaimsIdentityService claimsIdentityService,
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
        catch
        {
            await UnitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    protected abstract ValueTask<TResponse> HandleCommandAsync( TRequest request, CancellationToken cancellationToken );
}