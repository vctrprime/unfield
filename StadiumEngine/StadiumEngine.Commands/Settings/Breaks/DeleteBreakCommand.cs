using Mediator;
using StadiumEngine.DTO.Settings.Breaks;

namespace StadiumEngine.Commands.Settings.Breaks;

public sealed class DeleteBreakCommand : BaseCommand, IRequest<DeleteBreakDto>
{
    public int BreakId { get; set; }
}