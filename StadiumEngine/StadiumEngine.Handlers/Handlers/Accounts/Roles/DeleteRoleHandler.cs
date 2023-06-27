using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Application.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

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
        await _commandService.DeleteRoleAsync( request.RoleId, _legalId, _userId );

        return new DeleteRoleDto();
    }
}