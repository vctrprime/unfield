using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Core.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

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