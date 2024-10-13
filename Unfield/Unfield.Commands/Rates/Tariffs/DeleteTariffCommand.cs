using Mediator;
using Unfield.DTO.Rates.Tariffs;

namespace Unfield.Commands.Rates.Tariffs;

public class DeleteTariffCommand : BaseCommand, IRequest<DeleteTariffDto>
{
    public int TariffId { get; set; }
}