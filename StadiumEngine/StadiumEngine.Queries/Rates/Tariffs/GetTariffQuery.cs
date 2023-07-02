using Mediator;
using StadiumEngine.DTO.Rates.Tariffs;

namespace StadiumEngine.Queries.Rates.Tariffs;

public sealed class GetTariffQuery : BaseQuery, IRequest<TariffDto>
{
    public int TariffId { get; set; }
}