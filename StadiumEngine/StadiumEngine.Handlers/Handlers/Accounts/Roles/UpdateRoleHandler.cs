using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class UpdateRoleHandler : BaseCommandHandler<UpdateRoleCommand, UpdateRoleDto>
{
    private readonly IRoleCommandService _commandService;

    public UpdateRoleHandler(
        IRoleCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<UpdateRoleDto> HandleCommandAsync( UpdateRoleCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.UpdateRoleAsync(
            request.Id,
            _stadiumGroupId,
            _userId,
            request.Name,
            request.Description );
        return new UpdateRoleDto();
    }
}