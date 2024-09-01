using StadiumEngine.Commands.Admin;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Handlers.Facades.Accounts.StadiumGroups;

internal interface IChangeStadiumGroupFacade
{
    Task<User?> ChangeAsync( ChangeStadiumGroupCommand request, int userId );
}