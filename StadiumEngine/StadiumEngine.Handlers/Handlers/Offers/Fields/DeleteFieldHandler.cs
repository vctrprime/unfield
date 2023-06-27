using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Application.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class DeleteFieldHandler : BaseCommandHandler<DeleteFieldCommand, DeleteFieldDto>
{
    private readonly IFieldCommandService _commandService;

    public DeleteFieldHandler(
        IFieldCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<DeleteFieldDto> HandleCommandAsync( DeleteFieldCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.DeleteFieldAsync( request.FieldId, _currentStadiumId );
        return new DeleteFieldDto();
    }
}