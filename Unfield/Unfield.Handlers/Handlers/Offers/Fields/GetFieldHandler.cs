using AutoMapper;
using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.Fields;
using Unfield.Queries.Offers.Fields;

namespace Unfield.Handlers.Handlers.Offers.Fields;

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