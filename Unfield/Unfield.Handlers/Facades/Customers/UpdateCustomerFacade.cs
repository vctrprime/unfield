using Unfield.Commands.Customers;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Customers;
using Unfield.Domain.Services.Core.Customers;
using Unfield.DTO.Customers;

namespace Unfield.Handlers.Facades.Customers;

internal class UpdateCustomerFacade : IUpdateCustomerFacade
{
    private readonly ICustomerQueryService _queryService;
    private readonly ICustomerCommandService _commandService;

    public UpdateCustomerFacade( ICustomerQueryService queryService, ICustomerCommandService commandService )
    {
        _queryService = queryService;
        _commandService = commandService;
    }


    public async Task<Customer> UpdateAsync( UpdateCustomerCommand request, int customerId )
    {
        Customer? customer = await _queryService.GetCustomerAsync( customerId );
        if ( customer == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        customer.FirstName = request.Data.FirstName;
        customer.LastName = request.Data.LastName;
        
        _commandService.Update( customer );

        return customer;
    }
}