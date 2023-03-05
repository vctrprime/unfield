using AutoMapper;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Queries.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class GetFieldsHandler : BaseRequestHandler<GetFieldsQuery, List<FieldDto>>
{
    private readonly IFieldQueryFacade _fieldFacade;

    public GetFieldsHandler(
        IFieldQueryFacade fieldFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService ) : base( mapper, claimsIdentityService )
    {
        _fieldFacade = fieldFacade;
    }

    public override async ValueTask<List<FieldDto>> Handle( GetFieldsQuery request,
        CancellationToken cancellationToken )
    {
        var fields = await _fieldFacade.GetByStadiumId( _currentStadiumId );

        var fieldsDto = Mapper.Map<List<FieldDto>>( fields );

        return GetSortingFields( fieldsDto );
    }

    private List<FieldDto> GetSortingFields( List<FieldDto> fieldsDto )
    {
        var sortedFieldsDto = new List<FieldDto>();

        foreach (var fieldDto in fieldsDto.Where( x => !x.ParentFieldId.HasValue ).OrderBy( x => x.Id ))
        {
            sortedFieldsDto.Add( fieldDto );
            var sortedChildren = fieldsDto.Where( x => x.ParentFieldId == fieldDto.Id ).OrderBy( x => x.Id ).ToList();

            if (!sortedChildren.Any()) continue;

            var lastChild = sortedChildren.Last();
            lastChild.IsLastChild = true;

            sortedFieldsDto.AddRange( sortedChildren );
        }

        return sortedFieldsDto;
    }
}