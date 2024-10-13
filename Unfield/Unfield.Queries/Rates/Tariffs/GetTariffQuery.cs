using Mediator;
using Unfield.DTO.Rates.Tariffs;

namespace Unfield.Queries.Rates.Tariffs;

public sealed class GetTariffQuery : BaseQuery, IRequest<TariffDto>
{
    public int TariffId { get; set; }
}