using Mediator;
using StadiumEngine.DTO.Offers.Fields;

namespace StadiumEngine.Commands.Offers.Fields;

public sealed class DeleteFieldCommand : IRequest<DeleteFieldDto>
{
    public int FieldId { get; set; }
}