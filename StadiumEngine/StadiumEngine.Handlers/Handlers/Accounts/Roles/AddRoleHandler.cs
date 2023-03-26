using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class AddRoleHandler : BaseCommandHandler<AddRoleCommand, AddRoleDto>
{
    private readonly IRoleCommandFacade _roleFacade;

    public AddRoleHandler(
        IRoleCommandFacade roleFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _roleFacade = roleFacade;
    }

    protected override async ValueTask<AddRoleDto> HandleCommandAsync( AddRoleCommand request,
        CancellationToken cancellationToken )
    {
        Role? role = Mapper.Map<Role>( request );
        role.LegalId = _legalId;
        role.UserCreatedId = _userId;

        _roleFacade.AddRole( role );

        return await Task.Run( () => new AddRoleDto(), cancellationToken );
    }
}