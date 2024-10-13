using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Roles;
using Unfield.Commands.Accounts.Roles;

namespace Unfield.Handlers.Handlers.Accounts.Roles;

internal sealed class ToggleRolePermissionHandler : BaseCommandHandler<ToggleRolePermissionCommand, ToggleRolePermissionDto>
{
    private readonly IRoleCommandService _commandService;

    public ToggleRolePermissionHandler(
        IRoleCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork
    ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<ToggleRolePermissionDto> HandleCommandAsync( ToggleRolePermissionCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.ToggleRolePermissionAsync(
            request.RoleId,
            request.PermissionId,
            _stadiumGroupId,
            _userId );

        return new ToggleRolePermissionDto();
    }
}