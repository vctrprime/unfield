using Mediator;
using Unfield.DTO.Rates.Prices;

namespace Unfield.Queries.Rates.Prices;

public sealed class GetPricesQuery  : BaseQuery, IRequest<List<PriceDto>>
{
}
