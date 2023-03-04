using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class UpdateUserHandler : BaseCommandHandler<UpdateUserCommand, UpdateUserDto>
{
    private readonly IUserFacade _userFacade;

    public UpdateUserHandler(
        IUserFacade userFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _userFacade = userFacade;
    }
    
    protected override async ValueTask<UpdateUserDto> HandleCommand(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == _userId) throw new DomainException(ErrorsKeys.ModifyCurrentUser);

        var user = await _userFacade.GetUser(request.Id);
        if (user == null || _legalId != user.LegalId) throw new DomainException(ErrorsKeys.UserNotFound);
        
        user.Name = request.Name;
        user.Description = request.Description;
        user.RoleId = request.RoleId;
        user.LastName = request.LastName;

        await _userFacade.UpdateUser(user, _legalId);

        return new UpdateUserDto();
    }
}