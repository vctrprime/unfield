using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Application.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class AddFieldHandler : BaseCommandHandler<AddFieldCommand, AddFieldDto>
{
    private readonly IFieldCommandService _commandService;

    public AddFieldHandler(
        IFieldCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<AddFieldDto> HandleCommandAsync( AddFieldCommand request,
        CancellationToken cancellationToken )
    {
        Field? field = Mapper.Map<Field>( request );
        field.StadiumId = _currentStadiumId;
        field.UserCreatedId = _userId;

        await _commandService.AddFieldAsync( field, request.Images, _legalId );

        return new AddFieldDto();
    }
}