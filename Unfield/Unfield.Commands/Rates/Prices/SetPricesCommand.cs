using Mediator;
using Unfield.DTO.Rates.Prices;

namespace Unfield.Commands.Rates.Prices;

public sealed class SetPricesCommand : BaseCommand, IRequest<SetPricesDto>
{
    public List<PriceDto> Prices { get; set; } = new();
}
