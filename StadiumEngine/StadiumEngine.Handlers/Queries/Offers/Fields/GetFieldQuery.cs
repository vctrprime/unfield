using Mediator;
using StadiumEngine.DTO.Offers.Fields;

namespace StadiumEngine.Handlers.Queries.Offers.Fields;

public sealed class GetFieldQuery : IRequest<FieldDto>
{
    public GetFieldQuery( int fieldId )
    {
        FieldId = fieldId;
    }

    public int FieldId { get; }
}