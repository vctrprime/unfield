using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Core.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Commands.Offers.Inventories;

namespace StadiumEngine.Handlers.Handlers.Offers.Inventories;

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