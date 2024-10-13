using Unfield.DTO.Accounts.Users;
using Unfield.Commands.Accounts.Users;

namespace Unfield.Handlers.Facades.Accounts.Users;

internal interface IUpdateUserFacade
{
    Task<UpdateUserDto> UpdateAsync( UpdateUserCommand request, int userId, int stadiumGroupId );
}