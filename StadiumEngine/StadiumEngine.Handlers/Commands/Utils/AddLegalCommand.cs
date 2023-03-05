using Mediator;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Utils;

namespace StadiumEngine.Handlers.Commands.Utils;

public sealed class AddLegalCommand : IRequest<AddLegalDto>
{
    public string Inn { get; set; }

    public string HeadName { get; set; }

    public int CityId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string Language { get; set; }

    public AddLegalCommandSuperuser Superuser { get; set; }
    public List<AddLegalCommandStadium> Stadiums { get; set; }
}

public sealed class AddLegalCommandSuperuser
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}

public sealed class AddLegalCommandStadium
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Address { get; set; }
}