using Unfield.Commands.Admin;
using Unfield.Domain;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;

namespace Unfield.Handlers.Facades.Accounts.StadiumGroups;

internal class ChangeStadiumGroupFacade : IChangeStadiumGroupFacade
{
    private readonly IUserCommandService _commandService;
    private readonly IUserQueryService _queryService;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeStadiumGroupFacade( IUserCommandService commandService, IUserQueryService queryService, IUnitOfWork unitOfWork )
    {
        _commandService = commandService;
        _queryService = queryService;
        _unitOfWork = unitOfWork;
    }

    public async Task<User?> ChangeAsync( ChangeStadiumGroupCommand request, int userId )
    {
        await _commandService.ChangeStadiumGroupAsync( userId, request.StadiumGroupId );

        await _unitOfWork.SaveChangesAsync();

        User? user = await _queryService.GetUserAsync( userId );

        return user;
    }
}