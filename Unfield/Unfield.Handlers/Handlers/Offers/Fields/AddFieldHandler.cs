using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.Fields;
using Unfield.Commands.Offers.Fields;

namespace Unfield.Handlers.Handlers.Offers.Fields;

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

        await _commandService.AddFieldAsync( field, request.Images, _stadiumGroupId );

        return new AddFieldDto();
    }
}