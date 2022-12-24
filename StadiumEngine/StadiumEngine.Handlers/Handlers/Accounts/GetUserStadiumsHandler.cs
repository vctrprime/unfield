using AutoMapper;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;
using StadiumEngine.Repositories.Abstract.Accounts;
using StadiumEngine.Services.Auth.Abstract;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class GetUserStadiumsHandler :  BaseRequestHandler<GetUserStadiumsQuery, List<UserStadiumDto>>
{
    private readonly IUserStadiumRepository _repository;

    public GetUserStadiumsHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUserStadiumRepository repository) : base(mapper, claimsIdentityService)
    {
        _repository = repository;
    }
    
    public override async ValueTask<List<UserStadiumDto>> Handle(GetUserStadiumsQuery request, CancellationToken cancellationToken)
    {
        var userId = ClaimsIdentityService.GetUserId();
        var stadiums = await _repository.Get(userId);

        var stadiumsDto = Mapper.Map<List<UserStadiumDto>>(stadiums);

        var stadiumId = ClaimsIdentityService.GetCurrentStadiumId();
        stadiumsDto.First(s => s.Id == stadiumId).IsCurrent = true;

        return stadiumsDto;
    }
}