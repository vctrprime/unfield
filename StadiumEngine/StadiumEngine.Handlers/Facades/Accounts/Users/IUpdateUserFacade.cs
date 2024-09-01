using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Facades.Accounts.Users;

internal interface IUpdateUserFacade
{
    Task<UpdateUserDto> UpdateAsync( UpdateUserCommand request, int userId, int stadiumGroupId );
}