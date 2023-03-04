using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class UpdateFieldHandler : BaseCommandHandler<UpdateFieldCommand, UpdateFieldDto>
{
    private readonly IFieldFacade _fieldFacade;
        
    public UpdateFieldHandler(
        IFieldFacade fieldFacade,
        IMapper mapper, 
        IClaimsIdentityService claimsIdentityService, 
        IUnitOfWork unitOfWork) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _fieldFacade = fieldFacade;
    }

    protected override async ValueTask<UpdateFieldDto> HandleCommand(UpdateFieldCommand request, CancellationToken cancellationToken)
    {
        var field = await _fieldFacade.GetByFieldId(request.Id, _currentStadiumId);

        if (field == null) throw new DomainException(ErrorsKeys.FieldNotFound);
        
        field.Name = request.Name;
        field.Description = request.Description;
        field.Width = request.Width;
        field.Length = request.Length;
        field.ParentFieldId = request.ParentFieldId;
        field.CoveringType = request.CoveringType;
        field.IsActive = request.IsActive;
        field.PriceGroupId = request.PriceGroupId;
        field.UserModifiedId = _userId;

        await _fieldFacade.UpdateField(field, request.Images, request.SportKinds);
        
        return new UpdateFieldDto();
    }
}