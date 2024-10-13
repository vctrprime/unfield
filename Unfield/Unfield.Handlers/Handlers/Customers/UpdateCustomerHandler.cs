using AutoMapper;
using Unfield.Commands.Customers;
using Unfield.Domain;
using Unfield.Domain.Entities.Customers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Customers;
using Unfield.Handlers.Facades.Customers;

namespace Unfield.Handlers.Handlers.Customers;

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