using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Queries.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class GetFieldHandler : BaseRequestHandler<GetFieldQuery, FieldDto>
{
    private readonly IFieldFacade _fieldFacade;

    public GetFieldHandler(
        IFieldFacade fieldFacade, 
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService) : base(mapper, claimsIdentityService)
    {
        _fieldFacade = fieldFacade;
    }
    
    public override async ValueTask<FieldDto> Handle(GetFieldQuery request, CancellationToken cancellationToken)
    {
        var field = await _fieldFacade.GetByFieldId(request.FieldId, _currentStadiumId);

        if (field == null) throw new DomainException(ErrorsKeys.FieldNotFound);

        var fieldDto = Mapper.Map<FieldDto>(field);

        return fieldDto;
    }
}