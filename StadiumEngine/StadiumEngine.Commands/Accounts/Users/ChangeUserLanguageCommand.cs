using Mediator;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Commands.Accounts.Users;

public sealed class ChangeUserLanguageCommand : IRequest<ChangeUserLanguageDto>
{
    public string Language { get; set; }
}