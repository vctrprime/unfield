using StadiumEngine.Commands.Customers;
using StadiumEngine.Domain.Entities.Customers;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Handlers.Facades.Customers;

internal interface IUpdateCustomerFacade
{
    Task<Customer> UpdateAsync(UpdateCustomerCommand request, int customerId );
}