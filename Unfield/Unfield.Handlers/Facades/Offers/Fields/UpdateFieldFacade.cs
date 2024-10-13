using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Services.Core.Offers;
using Unfield.DTO.Offers.Fields;
using Unfield.Commands.Offers.Fields;

namespace Unfield.Handlers.Facades.Offers.Fields;

internal class UpdateFieldFacade : IUpdateFieldFacade
{
    private readonly IFieldCommandService _commandService;
    private readonly IFieldQueryService _queryService;

    public UpdateFieldFacade( IFieldQueryService queryService, IFieldCommandService commandService )
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    public async Task<UpdateFieldDto> UpdateAsync( UpdateFieldCommand request, int stadiumId, int userId )
    {
        Field? field = await _queryService.GetByFieldIdAsync( request.Id, stadiumId );

        if ( field == null )
        {
            throw new DomainException( ErrorsKeys.FieldNotFound );
        }

        field.Name = request.Name;
        field.Description = request.Description;
        field.Width = request.Width;
        field.Length = request.Length;
        field.ParentFieldId = request.ParentFieldId;
        field.CoveringType = request.CoveringType;
        field.IsActive = request.IsActive;
        field.PriceGroupId = request.PriceGroupId;
        field.UserModifiedId = userId;

        await _commandService.UpdateFieldAsync( field, request.Images, request.SportKinds );

        return new UpdateFieldDto();
    }
}