using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Commands.Accounts;

public sealed class ChangeStadiumCommand : IRequest<AuthorizeUserDto?>
{
    public int StadiumId { get; set; }
}