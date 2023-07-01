using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Queries.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class GetFieldHandler : BaseRequestHandler<GetFieldQuery, FieldDto>
{
    private readonly IFieldQueryService _queryService;

    public GetFieldHandler(
        IFieldQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<FieldDto> Handle( GetFieldQuery request, CancellationToken cancellationToken )
    {
        Field? field = await _queryService.GetByFieldIdAsync( request.FieldId, _currentStadiumId );

        if ( field == null )
        {
            throw new DomainException( ErrorsKeys.FieldNotFound );
        }

        FieldDto? fieldDto = Mapper.Map<FieldDto>( field );

        return fieldDto;
    }
}