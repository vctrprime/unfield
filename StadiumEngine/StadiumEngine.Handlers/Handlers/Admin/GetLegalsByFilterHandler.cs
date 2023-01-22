using AutoMapper;
using StadiumEngine.Domain.Repositories.Accounts;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Admin;
using StadiumEngine.Handlers.Queries.Admin;

namespace StadiumEngine.Handlers.Handlers.Admin;

internal sealed class GetLegalsByFilterHandler : BaseRequestHandler<GetLegalsByFilterQuery, List<LegalDto>>
{
    private readonly ILegalRepository _repository;

    public GetLegalsByFilterHandler(IMapper mapper, IClaimsIdentityService? claimsIdentityService, IUnitOfWork unitOfWork, ILegalRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }
    
    public override async ValueTask<List<LegalDto>> Handle(GetLegalsByFilterQuery request, CancellationToken cancellationToken)
    {
        var legals = await _repository.GetByFilter(request.SearchString);

        var legalsDto = Mapper.Map <List<LegalDto>>(legals);

        return legalsDto;
    }
}