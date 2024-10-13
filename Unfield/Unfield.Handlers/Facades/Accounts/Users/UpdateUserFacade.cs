using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.DTO.Accounts.Users;
using Unfield.Commands.Accounts.Users;

namespace Unfield.Handlers.Facades.Accounts.Users;

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