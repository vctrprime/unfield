using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.DTO.Accounts.Users;

namespace StadiumEngine.Handlers.Facades.Accounts.Users;

internal interface IAddUserFacade
{
    Task<AddUserDto> Add( User user, IUnitOfWork unitOfWork );
}