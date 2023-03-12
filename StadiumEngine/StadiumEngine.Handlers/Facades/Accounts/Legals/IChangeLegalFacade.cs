using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Commands.Admin;

namespace StadiumEngine.Handlers.Facades.Accounts.Legals;

internal interface IChangeLegalFacade
{
    Task<User?> Change( ChangeLegalCommand request, int userId, IUnitOfWork unitOfWork );
}