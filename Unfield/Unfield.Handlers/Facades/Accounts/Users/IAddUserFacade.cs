using Unfield.Domain;
using Unfield.Domain.Entities.Accounts;
using Unfield.DTO.Accounts.Users;

namespace Unfield.Handlers.Facades.Accounts.Users;

internal interface IAddUserFacade
{
    Task<AddUserDto> AddAsync( User user );
}