using Mediator;
using StadiumEngine.DTO.Rates.Tariffs;

namespace StadiumEngine.Commands.Rates.Tariffs;

public class DeleteTariffCommand : IRequest<DeleteTariffDto>
{
    public DeleteTariffCommand( int tariffId )
    {
        TariffId = tariffId;
    }

    public int TariffId { get; }
}