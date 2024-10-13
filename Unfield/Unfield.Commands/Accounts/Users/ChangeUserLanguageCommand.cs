using Mediator;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Commands.Accounts.Users;

public sealed class ChangeUserLanguageCommand : BaseCommand, IRequest<ChangeUserLanguageDto>
{
    public string Language { get; set; } = null!;
}