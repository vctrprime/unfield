using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class GetUserStadiumsHandler :  BaseRequestHandler<GetUserStadiumsQuery, List<UserStadiumDto>>
{
    private readonly IStadiumRepository _repository;

    public GetUserStadiumsHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IStadiumRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }

    public override async ValueTask<List<UserStadiumDto>> Handle(GetUserStadiumsQuery request, CancellationToken cancellationToken)
    {
        var isSuperuser = ClaimsIdentityService.GetIsSuperuser();

        List<Stadium> stadiums;
        if (isSuperuser)
        {
            var legalId = ClaimsIdentityService.GetLegalId();
            stadiums = await _repository.GetForLegal(legalId);
        }
        else
        {
            var roleId = ClaimsIdentityService.GetRoleId();
            stadiums = await _repository.GetForRole(roleId);
        }
        
        var stadiumsDto = Mapper.Map<List<UserStadiumDto>>(stadiums);

        var stadiumId = ClaimsIdentityService.GetCurrentStadiumId();
        stadiumsDto.First(s => s.Id == stadiumId).IsCurrent = true;

        return stadiumsDto;
    }
}