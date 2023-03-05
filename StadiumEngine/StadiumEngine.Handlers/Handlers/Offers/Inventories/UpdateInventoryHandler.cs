using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.Handlers.Commands.Offers.Inventories;
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

    protected override async ValueTask<UpdateInventoryDto> HandleCommand( UpdateInventoryCommand request,
        CancellationToken cancellationToken )
    {
        return await _facade.Update( request, _currentStadiumId, _userId );
    }
}