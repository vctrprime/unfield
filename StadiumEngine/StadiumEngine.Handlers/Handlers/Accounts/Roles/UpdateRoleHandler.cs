using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Handlers.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class UpdateRoleHandler : BaseRequestHandler<UpdateRoleCommand, UpdateRoleDto>
{
    private readonly IRoleFacade _roleFacade;
    
    public UpdateRoleHandler(
        IRoleFacade roleFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _roleFacade = roleFacade;
    }
    
    public override async ValueTask<UpdateRoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        await _roleFacade.UpdateRole(request.Id, _legalId, _userId, request.Name, request.Description);
        await UnitOfWork.SaveChanges();

        return new UpdateRoleDto();
    }
}