using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Commands.Admin;

namespace StadiumEngine.Handlers.Facades.Accounts.Legals;

internal class ChangeLegalFacade : IChangeLegalFacade
{
    private readonly IUserCommandService _commandService;
    private readonly IUserQueryService _queryService;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeLegalFacade( IUserCommandService commandService, IUserQueryService queryService, IUnitOfWork unitOfWork )
    {
        _commandService = commandService;
        _queryService = queryService;
        _unitOfWork = unitOfWork;
    }

    public async Task<User?> ChangeAsync( ChangeLegalCommand request, int userId )
    {
        await _commandService.ChangeLegalAsync( userId, request.LegalId );

        await _unitOfWork.SaveChangesAsync();

        User? user = await _queryService.GetUserAsync( userId );

        return user;
    }
}