using Mediator;
using Unfield.DTO.Offers.Fields;

namespace Unfield.Commands.Offers.Fields;

public sealed class DeleteFieldCommand : BaseCommand, IRequest<DeleteFieldDto>
{
    public int FieldId { get; set; }
}