using AutoMapper;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Accounts;
using StadiumEngine.Handlers.Queries.Accounts;

namespace StadiumEngine.Handlers.Handlers.Accounts;

internal sealed class GetStadiumsHandler : BaseRequestHandler<GetStadiumsQuery, List<StadiumDto>>
{
    private readonly IStadiumRepository _repository;

    public GetStadiumsHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IStadiumRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }
    
    public override async ValueTask<List<StadiumDto>> Handle(GetStadiumsQuery request, CancellationToken cancellationToken)
    {
        var stadiums = await _repository.GetForLegal(_legalId);

        var stadiumsDto = Mapper.Map<List<StadiumDto>>(stadiums);

        return stadiumsDto;
    }
}