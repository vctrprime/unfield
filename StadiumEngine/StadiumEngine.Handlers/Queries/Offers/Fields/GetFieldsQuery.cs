using Mediator;
using StadiumEngine.DTO.Offers.Fields;

namespace StadiumEngine.Handlers.Queries.Offers.Fields;

public sealed class GetFieldsQuery : IRequest<List<FieldDto>>
{
}