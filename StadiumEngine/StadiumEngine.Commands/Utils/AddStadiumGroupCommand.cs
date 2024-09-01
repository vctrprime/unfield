using Mediator;
using StadiumEngine.DTO.Utils;

namespace StadiumEngine.Commands.Utils;

public sealed class AddStadiumGroupCommand : BaseCommand, IRequest<AddStadiumGroupDto>
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Language { get; set; } = null!;

    public AddStadiumGroupCommandSuperuser Superuser { get; set; } = null!;
    public List<AddStadiumGroupCommandStadium> Stadiums { get; set; } = null!;
}

public sealed class AddStadiumGroupCommandSuperuser
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}

public sealed class AddStadiumGroupCommandStadium
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Address { get; set; } = null!;
    public int CityId { get; set; }
}