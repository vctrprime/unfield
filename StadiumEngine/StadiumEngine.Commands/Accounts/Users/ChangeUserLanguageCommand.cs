using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Accounts.Users;

public sealed class ChangeUserLanguageCommand : BaseCommand, IRequest<ChangeUserLanguageDto>
{
    public string Language { get; set; } = null!;
}