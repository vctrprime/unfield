using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Commands.Admin;

namespace StadiumEngine.Handlers.Facades.Accounts.Legals;

internal interface IChangeLegalFacade
{
    Task<User?> ChangeAsync( ChangeLegalCommand request, int userId );
}