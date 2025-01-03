using Mediator;
using Unfield.DTO.Rates.Tariffs;

namespace Unfield.Commands.Rates.Tariffs;

public sealed class AddTariffCommand : BaseCommand, IRequest<AddTariffDto>
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
    public List<TariffDayIntervalDto> DayIntervals { get; set; } = new();
    public List<PromoCodeDto> PromoCodes { get; set; } = new();
}