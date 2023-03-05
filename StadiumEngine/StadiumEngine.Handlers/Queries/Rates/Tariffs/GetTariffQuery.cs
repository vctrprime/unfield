using Mediator;
using StadiumEngine.DTO.Rates.Tariffs;

namespace StadiumEngine.Handlers.Queries.Rates.Tariffs;

public sealed class GetTariffQuery : IRequest<TariffDto>
{
    public GetTariffQuery( int tariffId )
    {
        TariffId = tariffId;
    }

    public int TariffId { get; }
}