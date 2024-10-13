using Mediator;
using Unfield.DTO.Offers.Fields;

namespace Unfield.Queries.Offers.Fields;

public sealed class GetFieldQuery : BaseQuery, IRequest<FieldDto>
{
    public int FieldId { get; set; }
}