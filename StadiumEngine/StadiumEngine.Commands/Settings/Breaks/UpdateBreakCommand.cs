using Mediator;
using StadiumEngine.DTO.Settings.Breaks;

namespace StadiumEngine.Commands.Settings.Breaks;

public sealed class UpdateBreakCommand : BaseCommand, IRequest<UpdateBreakDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public decimal StartHour { get; set; }
    public decimal EndHour { get; set; }
    public List<int> SelectedFields { get; set; } = new List<int>();
}