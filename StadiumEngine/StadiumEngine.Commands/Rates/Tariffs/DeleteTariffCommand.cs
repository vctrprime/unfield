using Mediator;
using StadiumEngine.DTO.Rates.Tariffs;

namespace StadiumEngine.Commands.Rates.Tariffs;

public class DeleteTariffCommand : IRequest<DeleteTariffDto>
{
    public int TariffId { get; set; }
}