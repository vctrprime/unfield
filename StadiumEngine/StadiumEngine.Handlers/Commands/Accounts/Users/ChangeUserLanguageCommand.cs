using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Commands.Accounts.Users;

public sealed class ChangeUserLanguageCommand : IRequest<ChangeUserLanguageDto>
{
    public string Language { get; }

    public ChangeUserLanguageCommand(string language)
    {
        Language = language;
    }
}