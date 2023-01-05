using AutoMapper;
using Mediator;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Handlers.Handlers;

internal abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected readonly IMapper Mapper;
    
    protected readonly int _userId;
    protected readonly int _legalId;
    protected readonly int _currentStadiumId;
    
    protected readonly IUnitOfWork UnitOfWork;

    protected BaseRequestHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork)
    {
        Mapper = mapper;

        _userId = claimsIdentityService.GetUserId();
        _legalId = claimsIdentityService.GetLegalId();
        _currentStadiumId = claimsIdentityService.GetCurrentStadiumId();
        
        UnitOfWork = unitOfWork;
    }
    public abstract ValueTask<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    protected void CheckRoleAccess(Role? role)
    {
        if (role == null || _legalId != role.LegalId) throw new DomainException("Указанная роль не найдена!");
    }
}