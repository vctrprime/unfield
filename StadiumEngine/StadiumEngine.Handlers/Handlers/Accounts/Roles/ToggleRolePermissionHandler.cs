using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Handlers.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class
    ToggleRolePermissionHandler : BaseCommandHandler<ToggleRolePermissionCommand, ToggleRolePermissionDto>
{
    private readonly IRoleCommandFacade _roleFacade;

    public ToggleRolePermissionHandler(
        IRoleCommandFacade roleFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork
    ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _roleFacade = roleFacade;
    }

    protected override async ValueTask<ToggleRolePermissionDto> HandleCommand( ToggleRolePermissionCommand request,
        CancellationToken cancellationToken )
    {
        await _roleFacade.ToggleRolePermission(
            request.RoleId,
            request.PermissionId,
            _legalId,
            _userId );

        return new ToggleRolePermissionDto();
    }
}