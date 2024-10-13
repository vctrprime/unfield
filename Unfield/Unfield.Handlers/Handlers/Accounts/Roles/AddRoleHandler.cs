using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Entities.Accounts;
using Unfield.Domain.Services.Core.Accounts;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Accounts.Roles;
using Unfield.Commands.Accounts.Roles;

namespace Unfield.Handlers.Handlers.Accounts.Roles;

internal sealed class AddRoleHandler : BaseCommandHandler<AddRoleCommand, AddRoleDto>
{
    private readonly IRoleCommandService _commandService;

    public AddRoleHandler(
        IRoleCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<AddRoleDto> HandleCommandAsync( AddRoleCommand request,
        CancellationToken cancellationToken )
    {
        Role? role = Mapper.Map<Role>( request );
        role.StadiumGroupId = _stadiumGroupId;
        role.UserCreatedId = _userId;

        _commandService.AddRole( role );

        return await Task.Run( () => new AddRoleDto(), cancellationToken );
    }
}