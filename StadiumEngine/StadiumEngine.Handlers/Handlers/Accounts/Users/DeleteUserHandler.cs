using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Handlers.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class DeleteUserHandler : BaseCommandHandler<DeleteUserCommand, DeleteUserDto>
{
    private readonly IUserFacade _userFacade;

    public DeleteUserHandler(
        IUserFacade userFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _userFacade = userFacade;
    }
    
    protected override async ValueTask<DeleteUserDto> HandleCommand(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userFacade.DeleteUser(request.UserId, _legalId, _userId);
        return new DeleteUserDto();
    }
}