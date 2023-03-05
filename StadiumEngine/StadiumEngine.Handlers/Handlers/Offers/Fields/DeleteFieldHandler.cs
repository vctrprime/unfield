using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class DeleteFieldHandler : BaseCommandHandler<DeleteFieldCommand, DeleteFieldDto>
{
    private readonly IFieldCommandFacade _fieldFacade;

    public DeleteFieldHandler(
        IFieldCommandFacade fieldFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _fieldFacade = fieldFacade;
    }

    protected override async ValueTask<DeleteFieldDto> HandleCommand( DeleteFieldCommand request,
        CancellationToken cancellationToken )
    {
        await _fieldFacade.DeleteField( request.FieldId, _currentStadiumId );
        return new DeleteFieldDto();
    }
}