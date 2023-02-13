using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Queries.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class GetFieldHandler : BaseRequestHandler<GetFieldQuery, FieldDto>
{
    private readonly IFieldRepository _repository;

    public GetFieldHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, IFieldRepository repository) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
    }
    
    public override async ValueTask<FieldDto> Handle(GetFieldQuery request, CancellationToken cancellationToken)
    {
        var field = await _repository.Get(request.FieldId, _currentStadiumId);

        if (field == null) throw new DomainException(ErrorsKeys.FieldNotFound);

        var fieldDto = Mapper.Map<FieldDto>(field);

        return fieldDto;
    }
}