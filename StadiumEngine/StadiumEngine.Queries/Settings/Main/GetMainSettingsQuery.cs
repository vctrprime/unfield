using Mediator;
using StadiumEngine.DTO.Settings.Main;

namespace StadiumEngine.Queries.Settings.Main;

public sealed class GetMainSettingsQuery : BaseQuery, IRequest<MainSettingsDto>
{
}