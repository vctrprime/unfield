using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class ToggleRoleStadiumHandler : BaseCommandHandler<ToggleRoleStadiumCommand, ToggleRoleStadiumDto>
{
    private readonly IRoleCommandFacade _roleFacade;

    public ToggleRoleStadiumHandler(
        IRoleCommandFacade roleFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork
    ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _roleFacade = roleFacade;
    }


    protected override async ValueTask<ToggleRoleStadiumDto> HandleCommand( ToggleRoleStadiumCommand request,
        CancellationToken cancellationToken )
    {
        await _roleFacade.ToggleRoleStadium(
            request.RoleId,
            request.StadiumId,
            _legalId,
            _userId );
        return new ToggleRoleStadiumDto();
    }
}