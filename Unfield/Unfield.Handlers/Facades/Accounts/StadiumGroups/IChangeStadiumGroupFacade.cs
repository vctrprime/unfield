using Unfield.Commands.Admin;
using Unfield.Domain.Entities.Accounts;

namespace Unfield.Handlers.Facades.Accounts.StadiumGroups;

internal interface IChangeStadiumGroupFacade
{
    Task<User?> ChangeAsync( ChangeStadiumGroupCommand request, int userId );
}