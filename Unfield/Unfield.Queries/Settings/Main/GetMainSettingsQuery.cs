using Mediator;
using Unfield.DTO.Settings.Main;

namespace Unfield.Queries.Settings.Main;

public sealed class GetMainSettingsQuery : BaseQuery, IRequest<MainSettingsDto>
{
    public int? StadiumId { get; set; }
}