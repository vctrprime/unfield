using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class ToggleRoleStadiumHandler : BaseCommandHandler<ToggleRoleStadiumCommand, ToggleRoleStadiumDto>
{
    private readonly IRoleCommandService _commandService;

    public ToggleRoleStadiumHandler(
        IRoleCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork
    ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }


    protected override async ValueTask<ToggleRoleStadiumDto> HandleCommandAsync( ToggleRoleStadiumCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.ToggleRoleStadiumAsync(
            request.RoleId,
            request.StadiumId,
            _legalId,
            _userId );
        return new ToggleRoleStadiumDto();
    }
}