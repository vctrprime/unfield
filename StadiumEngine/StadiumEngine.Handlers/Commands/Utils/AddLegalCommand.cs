using Mediator;
using StadiumEngine.DTO.Utils;

namespace StadiumEngine.Handlers.Commands.Utils;

public sealed class AddLegalCommand : IRequest<AddLegalDto>
{
    public string Inn { get; set; } = null!;

    public string HeadName { get; set; } = null!;

    public int CityId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Language { get; set; } = null!;

    public AddLegalCommandSuperuser Superuser { get; set; } = null!;
    public List<AddLegalCommandStadium> Stadiums { get; set; } = null!;
}

public sealed class AddLegalCommandSuperuser
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}

public sealed class AddLegalCommandStadium
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Address { get; set; } = null!;
}