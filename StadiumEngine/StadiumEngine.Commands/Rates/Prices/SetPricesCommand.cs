using Mediator;
using StadiumEngine.DTO.Rates.Prices;

namespace StadiumEngine.Commands.Rates.Prices;

public sealed class SetPricesCommand : IRequest<SetPricesDto>
{
    public List<PriceDto> Prices { get; set; } = new();
}
