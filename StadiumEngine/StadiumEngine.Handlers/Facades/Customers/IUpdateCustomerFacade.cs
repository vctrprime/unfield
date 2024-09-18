using StadiumEngine.Commands.Customers;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Handlers.Facades.Customers;

internal interface IUpdateCustomerFacade
{
    Task<UpdateCustomerDto> UpdateAsync(UpdateCustomerCommand request, int customerId );
}