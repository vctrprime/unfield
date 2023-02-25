using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts.Stadiums;
using StadiumEngine.Handlers.Queries.Accounts.Roles;

namespace StadiumEngine.Handlers.Handlers.Accounts.Roles;

internal sealed class GetStadiumsForRoleHandler : BaseRequestHandler<GetStadiumsForRoleQuery, List<StadiumDto>>
{
    private readonly IRoleFacade _roleFacade;

    public GetStadiumsForRoleHandler(
        IRoleFacade roleFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _roleFacade = roleFacade;
    }
    
    public override async ValueTask<List<StadiumDto>> Handle(GetStadiumsForRoleQuery request, CancellationToken cancellationToken)
    {
        var stadiums = await _roleFacade.GetStadiumsForRole(request.RoleId, _legalId);

        var stadiumsDto = Mapper.Map<List<StadiumDto>>(stadiums.Keys);
        stadiumsDto.ForEach(sd =>
        {
            sd.IsRoleBound = stadiums.FirstOrDefault(s => s.Key.Id == sd.Id).Value;
        });
        
        return stadiumsDto;
    }
}