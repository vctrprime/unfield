using AutoMapper;
using Mediator;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Handlers.Handlers;

internal abstract class BaseCommandHandler<TRequest, TResponse> : BaseRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly IUnitOfWork UnitOfWork;
    private readonly bool _transactional;

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
        if (!_transactional) return await HandleCommand( request, cancellationToken );

        try
        {
            await UnitOfWork.BeginTransaction();

            var result = await HandleCommand( request, cancellationToken );

            await UnitOfWork.CommitTransaction();

            return result;
        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }
    }

    protected abstract ValueTask<TResponse> HandleCommand( TRequest request, CancellationToken cancellationToken );
}