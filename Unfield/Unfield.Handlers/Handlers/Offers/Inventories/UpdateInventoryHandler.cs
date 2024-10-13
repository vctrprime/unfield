using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Offers.Inventories;
using Unfield.Commands.Offers.Inventories;
using Unfield.Handlers.Facades.Offers.Inventories;

namespace Unfield.Handlers.Handlers.Offers.Inventories;

internal sealed class UpdateInventoryHandler : BaseCommandHandler<UpdateInventoryCommand, UpdateInventoryDto>
{
    private readonly IUpdateInventoryFacade _facade;

    public UpdateInventoryHandler(
        IUpdateInventoryFacade facade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<UpdateInventoryDto> HandleCommandAsync( UpdateInventoryCommand request,
        CancellationToken cancellationToken ) =>
        await _facade.UpdateAsync( request, _currentStadiumId, _userId );
}