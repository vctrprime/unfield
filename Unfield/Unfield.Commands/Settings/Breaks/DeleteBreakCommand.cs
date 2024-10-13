using Mediator;
using Unfield.DTO.Settings.Breaks;

namespace Unfield.Commands.Settings.Breaks;

public sealed class DeleteBreakCommand : BaseCommand, IRequest<DeleteBreakDto>
{
    public int BreakId { get; set; }
}