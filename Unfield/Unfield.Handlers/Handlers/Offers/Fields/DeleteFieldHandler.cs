using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.Fields;
using Unfield.Commands.Offers.Fields;

namespace Unfield.Handlers.Handlers.Offers.Fields;

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