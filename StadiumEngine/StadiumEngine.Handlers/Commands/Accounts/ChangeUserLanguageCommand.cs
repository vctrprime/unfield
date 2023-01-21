using Mediator;
using StadiumEngine.DTO.Accounts;

namespace StadiumEngine.Handlers.Commands.Accounts;

public sealed class ChangeUserLanguageCommand : IRequest<ChangeUserLanguageDto>
{
    public string Language { get; }

    public ChangeUserLanguageCommand(string language)
    {
        Language = language;
    }
}