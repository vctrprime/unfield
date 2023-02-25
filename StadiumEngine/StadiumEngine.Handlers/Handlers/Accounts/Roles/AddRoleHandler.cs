using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Handlers.Commands.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class AddRoleHandler : BaseRequestHandler<AddRoleCommand, AddRoleDto>
{
    private readonly IRoleFacade _roleFacade;

    public AddRoleHandler(
        IRoleFacade roleFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _roleFacade = roleFacade;
    }
    
    public override async ValueTask<AddRoleDto> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var role = Mapper.Map<Role>(request);
        role.LegalId = _legalId;
        role.UserCreatedId = _userId;
        
        _roleFacade.AddRole(role);
        await UnitOfWork.SaveChanges();

        return new AddRoleDto();
    }
}