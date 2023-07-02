using Mediator;
using StadiumEngine.DTO.Offers.Fields;

namespace StadiumEngine.Queries.Offers.Fields;

public sealed class GetFieldsQuery : BaseQuery, IRequest<List<FieldDto>>
{
}