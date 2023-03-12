using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Facades.Offers.Fields;

internal class UpdateFieldFacade : IUpdateFieldFacade
{
    private readonly IFieldCommandFacade _commandFacade;
    private readonly IFieldQueryFacade _queryFacade;

    public UpdateFieldFacade( IFieldQueryFacade queryFacade, IFieldCommandFacade commandFacade )
    {
        _queryFacade = queryFacade;
        _commandFacade = commandFacade;
    }

    public async Task<UpdateFieldDto> Update( UpdateFieldCommand request, int stadiumId, int userId )
    {
        Field? field = await _queryFacade.GetByFieldId( request.Id, stadiumId );

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

        await _commandFacade.UpdateField( field, request.Images, request.SportKinds );

        return new UpdateFieldDto();
    }
}