using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.Inventories;
using Unfield.Commands.Offers.Inventories;

namespace Unfield.Handlers.Handlers.Offers.Inventories;

internal sealed class AddInventoryHandler : BaseCommandHandler<AddInventoryCommand, AddInventoryDto>
{
    private readonly IInventoryCommandService _commandService;

    public AddInventoryHandler(
        IInventoryCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<AddInventoryDto> HandleCommandAsync( AddInventoryCommand request,
        CancellationToken cancellationToken )
    {
        Inventory? inventory = Mapper.Map<Inventory>( request );
        inventory.StadiumId = _currentStadiumId;
        inventory.UserCreatedId = _userId;

        await _commandService.AddInventoryAsync( inventory, request.Images, _stadiumGroupId );

        return new AddInventoryDto();
    }
}