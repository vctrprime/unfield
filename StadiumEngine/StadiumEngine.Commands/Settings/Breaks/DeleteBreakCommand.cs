using Mediator;
using StadiumEngine.DTO.Settings.Breaks;

namespace StadiumEngine.Commands.Settings.Breaks;

public sealed class DeleteBreakCommand : IRequest<DeleteBreakDto>
{
    public int BreakId { get; set; }
}