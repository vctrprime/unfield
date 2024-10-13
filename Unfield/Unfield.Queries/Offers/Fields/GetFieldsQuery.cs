using Mediator;
using Unfield.DTO.Offers.Fields;

namespace Unfield.Queries.Offers.Fields;

public sealed class GetFieldsQuery : BaseQuery, IRequest<List<FieldDto>>
{
}