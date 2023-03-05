using Mediator;
using StadiumEngine.DTO.Rates.Tariffs;

namespace StadiumEngine.Handlers.Queries.Rates.Tariffs;

public sealed class GetTariffsQuery : IRequest<List<TariffDto>>
{
}