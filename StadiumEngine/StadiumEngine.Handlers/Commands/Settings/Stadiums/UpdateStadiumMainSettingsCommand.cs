using Mediator;
using StadiumEngine.DTO.Settings.Stadiums;

namespace StadiumEngine.Handlers.Commands.Settings.Stadiums;

public class UpdateStadiumMainSettingsCommand: IRequest<UpdateStadiumMainSettingsDto>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? OpenTime { get; set; }
    public string? CloseTime { get; set; }
}