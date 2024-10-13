using Mediator;
using Unfield.DTO.Rates.Tariffs;

namespace Unfield.Queries.Rates.Tariffs;

public sealed class GetTariffsQuery : BaseQuery, IRequest<List<TariffDto>>
{
}