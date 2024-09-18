using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Facades.Accounts.Users;

internal class UpdateUserFacade : IUpdateUserFacade
{
    private readonly IUserCommandService _commandService;
    private readonly IUserQueryService _queryService;

    public UpdateUserFacade( IUserQueryService queryService, IUserCommandService commandService )
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    public async Task<UpdateUserDto> UpdateAsync( UpdateUserCommand request, int userId, int stadiumGroupId )
    {
        if ( request.Id == userId )
        {
            throw new DomainException( ErrorsKeys.ModifyCurrentUser );
        }

        User? user = await _queryService.GetUserAsync( request.Id );
        if ( user == null || stadiumGroupId != user.StadiumGroupId )
        {
            throw new DomainException( ErrorsKeys.UserNotFound );
        }

        user.Name = request.Name;
        user.Description = request.Description;
        user.RoleId = request.RoleId;
        user.LastName = request.LastName;
        user.UserModifiedId = userId;
        
        _commandService.UpdateUser( user );

        return new UpdateUserDto();
    }
}