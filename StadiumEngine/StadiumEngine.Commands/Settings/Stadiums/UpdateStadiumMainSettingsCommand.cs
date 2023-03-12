using Mediator;
using StadiumEngine.DTO.Settings.Stadiums;

namespace StadiumEngine.Commands.Settings.Stadiums;

public class UpdateStadiumMainSettingsCommand: IRequest<UpdateStadiumMainSettingsDto>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int OpenTime { get; set; }
    public int CloseTime { get; set; }
}