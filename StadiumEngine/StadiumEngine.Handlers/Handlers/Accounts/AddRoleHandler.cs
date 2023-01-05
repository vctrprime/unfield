using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Commands.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class AddRoleHandler : BaseRequestHandler<AddRoleCommand, AddRoleDto>
{
    private readonly IRoleRepository _repository;

    public AddRoleHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IRoleRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }
    
    public override async ValueTask<AddRoleDto> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var role = Mapper.Map<Role>(request);
        role.LegalId = _legalId;
        role.UserCreatedId = _userId;
        
        _repository.Add(role);
        await UnitOfWork.SaveChanges();

        return new AddRoleDto();
    }
}