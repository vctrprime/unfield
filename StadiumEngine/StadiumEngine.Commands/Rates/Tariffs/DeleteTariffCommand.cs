using Mediator;
using StadiumEngine.DTO.Rates.Tariffs;

namespace StadiumEngine.Commands.Rates.Tariffs;

public class DeleteTariffCommand : BaseCommand, IRequest<DeleteTariffDto>
{
    public int TariffId { get; set; }
}