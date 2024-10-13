using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.Inventories;
using Unfield.Commands.Offers.Inventories;

namespace Unfield.Handlers.Handlers.Offers.Inventories;

internal sealed class DeleteInventoryHandler : BaseCommandHandler<DeleteInventoryCommand, DeleteInventoryDto>
{
    private readonly IInventoryCommandService _commandService;

    public DeleteInventoryHandler(
        IInventoryCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<DeleteInventoryDto> HandleCommandAsync( DeleteInventoryCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.DeleteInventoryAsync( request.InventoryId, _currentStadiumId );
        return new DeleteInventoryDto();
    }
}