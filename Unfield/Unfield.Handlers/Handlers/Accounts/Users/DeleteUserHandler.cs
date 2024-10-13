using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Users;
using Unfield.Commands.Accounts.Users;

namespace Unfield.Handlers.Handlers.Accounts.Users;

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
        await _commandService.DeleteUserAsync( request.UserId, _stadiumGroupId, _userId );
        return new DeleteUserDto();
    }
}