using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Commands.Offers.Inventories;
using StadiumEngine.Handlers.Facades.Offers.Inventories;

namespace StadiumEngine.Handlers.Handlers.Offers.Inventories;

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