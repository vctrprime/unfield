using AutoMapper;
using StadiumEngine.Commands.Customers;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Customers;
using StadiumEngine.Handlers.Facades.Customers;

namespace StadiumEngine.Handlers.Handlers.Customers;

internal sealed class UpdateCustomerHandler : BaseCustomerCommandHandler<UpdateCustomerCommand, AuthorizedCustomerDto>
{
    private readonly IUpdateCustomerFacade _facade;

    public UpdateCustomerHandler(
        IUpdateCustomerFacade facade,
        IMapper mapper,
        ICustomerClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<AuthorizedCustomerDto> HandleCommandAsync(
        UpdateCustomerCommand request,
        CancellationToken cancellationToken )
    {
        Customer customer = await _facade.UpdateAsync( request, _customerId );
        
        AuthorizedCustomerDto? customerDto = Mapper.Map<AuthorizedCustomerDto>( customer );

        return customerDto;
    }
        
}