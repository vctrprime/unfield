using AutoMapper;
using Mediator;
using StadiumEngine.Domain.Services.Identity;

namespace StadiumEngine.Handlers.Handlers;

internal abstract class BaseCustomerRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly int _customerId;
    protected readonly string _customerPhoneNumber;
    protected readonly int _customerStadiumGroupId;
    
    protected readonly IMapper Mapper;

    protected BaseCustomerRequestHandler( IMapper mapper, ICustomerClaimsIdentityService claimsIdentityService )
    {
        Mapper = mapper;
        
        _customerId = claimsIdentityService.GetCustomerId();
        _customerPhoneNumber = claimsIdentityService.GetCustomerPhoneNumber();
        _customerStadiumGroupId = claimsIdentityService.GetCustomerStadiumGroupId();
    }
    
    public abstract ValueTask<TResponse> Handle( TRequest request, CancellationToken cancellationToken );
}