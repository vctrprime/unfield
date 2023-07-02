using Mediator;
using StadiumEngine.DTO.Settings.Main;

namespace StadiumEngine.Commands.Settings.Main;

public class UpdateMainSettingsCommand : BaseCommand, IRequest<UpdateMainSettingsDto>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int OpenTime { get; set; }
    public int CloseTime { get; set; }
}