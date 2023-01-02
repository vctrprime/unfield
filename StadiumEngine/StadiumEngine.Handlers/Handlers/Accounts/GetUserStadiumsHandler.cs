using AutoMapper;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class GetUserStadiumsHandler :  BaseRequestHandler<GetUserStadiumsQuery, List<UserStadiumDto>>
{
    private readonly IUserStadiumRepository _repository;

    public GetUserStadiumsHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IUserStadiumRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }

    public override async ValueTask<List<UserStadiumDto>> Handle(GetUserStadiumsQuery request, CancellationToken cancellationToken)
    {
        var roleId = ClaimsIdentityService.GetUserId();
        var legalId = ClaimsIdentityService.GetLegalId();
        var isSuperuser = ClaimsIdentityService.GetIsSuperuser();
        
        var stadiums = await _repository.Get(roleId, legalId, isSuperuser);

        var stadiumsDto = Mapper.Map<List<UserStadiumDto>>(stadiums);

        var stadiumId = ClaimsIdentityService.GetCurrentStadiumId();
        stadiumsDto.First(s => s.Id == stadiumId).IsCurrent = true;

        return stadiumsDto;
    }
}