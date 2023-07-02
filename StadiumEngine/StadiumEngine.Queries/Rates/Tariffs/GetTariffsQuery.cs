using Mediator;
using StadiumEngine.DTO.Rates.Tariffs;

namespace StadiumEngine.Queries.Rates.Tariffs;

public sealed class GetTariffsQuery : BaseQuery, IRequest<List<TariffDto>>
{
}