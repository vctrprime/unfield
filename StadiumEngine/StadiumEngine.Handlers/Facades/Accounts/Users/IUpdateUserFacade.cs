using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Facades.Accounts.Users;

internal interface IUpdateUserFacade
{
    Task<UpdateUserDto> Update( UpdateUserCommand request, int userId, int legalId );
}