using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Handlers.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class DeleteRoleHandler : BaseRequestHandler<DeleteRoleCommand, DeleteRoleDto>
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


    public override async ValueTask<DeleteRoleDto> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        await _roleFacade.DeleteRole(request.RoleId, _legalId, _userId);
        await UnitOfWork.SaveChanges();

        return new DeleteRoleDto();
    }
}