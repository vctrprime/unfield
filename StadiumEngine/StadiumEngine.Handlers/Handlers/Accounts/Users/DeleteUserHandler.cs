using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class DeleteUserHandler : BaseCommandHandler<DeleteUserCommand, DeleteUserDto>
{
    private readonly IUserCommandFacade _userFacade;

    public DeleteUserHandler(
        IUserCommandFacade userFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _userFacade = userFacade;
    }

    protected override async ValueTask<DeleteUserDto> HandleCommandAsync( DeleteUserCommand request,
        CancellationToken cancellationToken )
    {
        await _userFacade.DeleteUserAsync( request.UserId, _legalId, _userId );
        return new DeleteUserDto();
    }
}