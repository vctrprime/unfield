using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

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