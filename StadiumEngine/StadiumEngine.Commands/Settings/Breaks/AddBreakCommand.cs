using Mediator;
using StadiumEngine.DTO.Settings.Breaks;

namespace StadiumEngine.Commands.Settings.Breaks;

public sealed class AddBreakCommand : IRequest<AddBreakDto>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public string StartHour { get; set; } = null!;
    public string EndHour { get; set; } = null!;
    public List<int> SelectedFields { get; set; } = new List<int>();
}