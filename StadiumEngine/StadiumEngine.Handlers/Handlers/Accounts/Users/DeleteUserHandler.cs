using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Users;
using StadiumEngine.Commands.Accounts.Users;

namespace StadiumEngine.Handlers.Handlers.Accounts.Users;

internal sealed class DeleteUserHandler : BaseCommandHandler<DeleteUserCommand, DeleteUserDto>
{
    private readonly IUserCommandService _commandService;

    public DeleteUserHandler(
        IUserCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<DeleteUserDto> HandleCommandAsync( DeleteUserCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.DeleteUserAsync( request.UserId, _legalId, _userId );
        return new DeleteUserDto();
    }
}