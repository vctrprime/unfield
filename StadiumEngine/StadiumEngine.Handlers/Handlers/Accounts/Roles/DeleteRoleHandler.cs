using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Handlers.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class DeleteRoleHandler : BaseCommandHandler<DeleteRoleCommand, DeleteRoleDto>
{
    private readonly IRoleFacade _roleFacade;

    public DeleteRoleHandler(
        IRoleFacade roleFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _roleFacade = roleFacade;
    }


    protected override async ValueTask<DeleteRoleDto> HandleCommand(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        await _roleFacade.DeleteRole(request.RoleId, _legalId, _userId);
        
        return new DeleteRoleDto();
    }
}