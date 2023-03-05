using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class AddFieldHandler : BaseCommandHandler<AddFieldCommand, AddFieldDto>
{
    private readonly IFieldCommandFacade _fieldFacade;

    public AddFieldHandler(
        IFieldCommandFacade fieldFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _fieldFacade = fieldFacade;
    }

    protected override async ValueTask<AddFieldDto> HandleCommand( AddFieldCommand request,
        CancellationToken cancellationToken )
    {
        var field = Mapper.Map<Field>( request );
        field.StadiumId = _currentStadiumId;
        field.UserCreatedId = _userId;

        await _fieldFacade.AddField( field, request.Images, _legalId );

        return new AddFieldDto();
    }
}