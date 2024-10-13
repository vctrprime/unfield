using Unfield.Commands.Customers;
using Unfield.Domain.Entities.Customers;
using Unfield.DTO.Customers;

namespace Unfield.Handlers.Facades.Customers;

internal interface IUpdateCustomerFacade
{
    Task<Customer> UpdateAsync(UpdateCustomerCommand request, int customerId );
}