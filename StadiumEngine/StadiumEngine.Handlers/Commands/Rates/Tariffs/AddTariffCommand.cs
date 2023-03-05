using Mediator;
using StadiumEngine.DTO.Rates.Tariffs;

namespace StadiumEngine.Handlers.Commands.Rates.Tariffs;

public sealed class AddTariffCommand : IRequest<AddTariffDto>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
    public List<string[]> DayIntervals { get; set; } = new();
}