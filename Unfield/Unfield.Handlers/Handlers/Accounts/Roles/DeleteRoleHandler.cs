using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Roles;
using Unfield.Commands.Accounts.Roles;

namespace Unfield.Handlers.Handlers.Accounts.Roles;

internal sealed class DeleteRoleHandler : BaseCommandHandler<DeleteRoleCommand, DeleteRoleDto>
{
    private readonly IRoleCommandService _commandService;

    public DeleteRoleHandler(
        IRoleCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }


    protected override async ValueTask<DeleteRoleDto> HandleCommandAsync( DeleteRoleCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.DeleteRoleAsync( request.RoleId, _stadiumGroupId, _userId );

        return new DeleteRoleDto();
    }
}