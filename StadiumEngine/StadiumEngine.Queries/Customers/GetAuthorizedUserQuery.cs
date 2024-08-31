using Mediator;
using StadiumEngine.DTO.Customers;

namespace StadiumEngine.Queries.Customers;

public sealed class GetAuthorizedCustomerQuery : BaseQuery, IRequest<AuthorizedCustomerDto?>
{
}