using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Commands.Admin;

namespace StadiumEngine.Handlers.Facades.Accounts.Legals;

internal class ChangeLegalFacade : IChangeLegalFacade
{
    private readonly IUserCommandFacade _commandFacade;
    private readonly IUserQueryFacade _queryFacade;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeLegalFacade( IUserCommandFacade commandFacade, IUserQueryFacade queryFacade, IUnitOfWork unitOfWork )
    {
        _commandFacade = commandFacade;
        _queryFacade = queryFacade;
        _unitOfWork = unitOfWork;
    }

    public async Task<User?> ChangeAsync( ChangeLegalCommand request, int userId )
    {
        await _commandFacade.ChangeLegalAsync( userId, request.LegalId );

        await _unitOfWork.SaveChangesAsync();

        User? user = await _queryFacade.GetUserAsync( userId );

        return user;
    }
}