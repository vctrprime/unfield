using Mediator;
using Unfield.DTO.Customers;

namespace Unfield.Queries.Customers;

public sealed class GetAuthorizedCustomerQuery : BaseQuery, IRequest<AuthorizedCustomerDto?>
{
}