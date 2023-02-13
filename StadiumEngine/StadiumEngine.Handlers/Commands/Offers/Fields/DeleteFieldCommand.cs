using Mediator;
using StadiumEngine.DTO.Offers.Fields;

namespace StadiumEngine.Handlers.Commands.Offers.Fields;

public sealed class DeleteFieldCommand: IRequest<DeleteFieldDto>
{
    public DeleteFieldCommand(int fieldId)
    {
        FieldId = fieldId;
    }
    
    public int FieldId { get; }
}