using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Handlers.Commands.Admin;

namespace StadiumEngine.Handlers.Facades.Accounts.Legals;

internal class ChangeLegalFacade : IChangeLegalFacade
{
    private readonly IUserQueryFacade _queryFacade;
    private readonly IUserCommandFacade _commandFacade;
    
    public ChangeLegalFacade(IUserQueryFacade queryFacade, IUserCommandFacade commandFacade)
    {
        _queryFacade = queryFacade;
        _commandFacade = commandFacade;
    }
    
    public async Task<User?> Change(ChangeLegalCommand request, int userId, IUnitOfWork unitOfWork)
    {
        await _commandFacade.ChangeLegal(userId, request.LegalId);
        
        await unitOfWork.SaveChanges();
        
        var user = await _queryFacade.GetUser(userId);

        return user;
    }
}