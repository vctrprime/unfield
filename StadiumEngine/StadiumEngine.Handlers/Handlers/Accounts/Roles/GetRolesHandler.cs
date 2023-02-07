using AutoMapper;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Roles;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.Handlers.Queries.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class GetRolesHandler : BaseRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IRoleRepository _repository;

    public GetRolesHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IRoleRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }
    
    public override async ValueTask<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _repository.GetAll(_legalId);

        var rolesDto = Mapper.Map<List<RoleDto>>(roles);

        return rolesDto;
    }
}