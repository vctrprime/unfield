using AutoMapper;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.DTO.Accounts.Stadiums;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.Handlers.Queries.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class GetStadiumsForRoleHandler : BaseRequestHandler<GetStadiumsForRoleQuery, List<StadiumDto>>
{
    private readonly IStadiumRepository _stadiumRepository;
    private readonly IRoleRepository _roleRepository;

    public GetStadiumsForRoleHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork, IStadiumRepository stadiumRepository,
        IRoleRepository roleRepository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _stadiumRepository = stadiumRepository;
        _roleRepository = roleRepository;
    }
    
    public override async ValueTask<List<StadiumDto>> Handle(GetStadiumsForRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.Get(request.RoleId);
        CheckRoleAccess(role);
        
        var stadiums = await _stadiumRepository.GetForLegal(_legalId);

        var stadiumsDto = Mapper.Map<List<StadiumDto>>(stadiums);
        stadiumsDto.ForEach(s =>
        {
            s.IsRoleBound = role.RoleStadiums.FirstOrDefault(rs => rs.RoleId == request.RoleId && s.Id == rs.StadiumId) != null;
        });
        
        return stadiumsDto;
    }
}