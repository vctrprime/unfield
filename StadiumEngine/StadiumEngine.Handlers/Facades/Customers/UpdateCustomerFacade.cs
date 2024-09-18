using StadiumEngine.Commands.Customers;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.Domain.Services.Core.Customers;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Handlers.Facades.Customers;

internal class UpdateCustomerFacade : IUpdateCustomerFacade
{
    private readonly ICustomerQueryService _queryService;
    private readonly ICustomerCommandService _commandService;

    public UpdateCustomerFacade( ICustomerQueryService queryService, ICustomerCommandService commandService )
    {
        _queryService = queryService;
        _commandService = commandService;
    }


    public async Task<UpdateCustomerDto> UpdateAsync( UpdateCustomerCommand request, int customerId )
    {
        Customer? customer = await _queryService.GetCustomerAsync( customerId );
        if ( customer == null )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        customer.FirstName = request.Data.FirstName;
        customer.LastName = request.Data.LastName;
        
        _commandService.Update( customer );

        return new UpdateCustomerDto();
    }
}