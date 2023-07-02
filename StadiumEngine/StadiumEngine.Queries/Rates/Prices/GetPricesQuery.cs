using Mediator;
using StadiumEngine.DTO.Rates.Prices;

namespace StadiumEngine.Queries.Rates.Prices;

public sealed class GetPricesQuery  : BaseQuery, IRequest<List<PriceDto>>
{
}
