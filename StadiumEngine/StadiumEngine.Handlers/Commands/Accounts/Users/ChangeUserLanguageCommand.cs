using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Commands.Accounts.Users;

public sealed class ChangeUserLanguageCommand : IRequest<ChangeUserLanguageDto>
{
    public ChangeUserLanguageCommand( string language )
    {
        Language = language;
    }

    public string Language { get; }
}