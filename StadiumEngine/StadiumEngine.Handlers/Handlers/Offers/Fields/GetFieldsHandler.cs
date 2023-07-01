using AutoMapper;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Queries.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class GetFieldsHandler : BaseRequestHandler<GetFieldsQuery, List<FieldDto>>
{
    private readonly IFieldQueryService _queryService;

    public GetFieldsHandler(
        IFieldQueryService queryService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _queryService = queryService;
    }

    public override async ValueTask<List<FieldDto>> Handle( GetFieldsQuery request,
        CancellationToken cancellationToken )
    {
        List<Field> fields = await _queryService.GetByStadiumIdAsync( _currentStadiumId );

        List<FieldDto>? fieldsDto = Mapper.Map<List<FieldDto>>( fields );

        return GetSortingFields( fieldsDto );
    }

    private List<FieldDto> GetSortingFields( List<FieldDto> fieldsDto )
    {
        List<FieldDto> sortedFieldsDto = new();

        foreach ( FieldDto fieldDto in fieldsDto.Where( x => !x.ParentFieldId.HasValue ).OrderBy( x => x.Id ) )
        {
            sortedFieldsDto.Add( fieldDto );
            List<FieldDto> sortedChildren =
                fieldsDto.Where( x => x.ParentFieldId == fieldDto.Id ).OrderBy( x => x.Id ).ToList();

            if ( !sortedChildren.Any() )
            {
                continue;
            }

            FieldDto lastChild = sortedChildren.Last();
            lastChild.IsLastChild = true;

            sortedFieldsDto.AddRange( sortedChildren );
        }

        return sortedFieldsDto;
    }
}