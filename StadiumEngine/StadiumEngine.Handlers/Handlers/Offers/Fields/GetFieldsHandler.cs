using AutoMapper;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Queries.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class GetFieldsHandler : BaseRequestHandler<GetFieldsQuery, List<FieldDto>>
{
    private readonly IFieldRepository _repository;

    public GetFieldsHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IFieldRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }
    
    public override async ValueTask<List<FieldDto>> Handle(GetFieldsQuery request, CancellationToken cancellationToken)
    {
        var fields = await _repository.GetAll(_currentStadiumId);

        var fieldsDto = Mapper.Map<List<FieldDto>>(fields);

        return fieldsDto;
    }
}