using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Facades.Accounts.Users;

internal class UpdateUserFacade : IUpdateUserFacade
{
    private readonly IUserQueryFacade _queryFacade;
    private readonly IUserCommandFacade _commandFacade;

    public UpdateUserFacade(IUserQueryFacade queryFacade, IUserCommandFacade commandFacade)
    {
        _queryFacade = queryFacade;
        _commandFacade = commandFacade;
    }
    
    public async Task<UpdateUserDto> Update(UpdateUserCommand request, int userId, int legalId)
    {
        if (request.Id == userId) throw new DomainException(ErrorsKeys.ModifyCurrentUser);

        var user = await _queryFacade.GetUser(request.Id);
        if (user == null || legalId != user.LegalId) throw new DomainException(ErrorsKeys.UserNotFound);
        
        user.Name = request.Name;
        user.Description = request.Description;
        user.RoleId = request.RoleId;
        user.LastName = request.LastName;
        user.UserModifiedId = userId;
        

        await _commandFacade.UpdateUser(user, legalId);

        return new UpdateUserDto();
    }
}