using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class UpdateRoleHandler : BaseCommandHandler<UpdateRoleCommand, UpdateRoleDto>
{
    private readonly IRoleCommandFacade _roleFacade;

    public UpdateRoleHandler(
        IRoleCommandFacade roleFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _roleFacade = roleFacade;
    }

    protected override async ValueTask<UpdateRoleDto> HandleCommandAsync( UpdateRoleCommand request,
        CancellationToken cancellationToken )
    {
        await _roleFacade.UpdateRoleAsync(
            request.Id,
            _legalId,
            _userId,
            request.Name,
            request.Description );
        return new UpdateRoleDto();
    }
}