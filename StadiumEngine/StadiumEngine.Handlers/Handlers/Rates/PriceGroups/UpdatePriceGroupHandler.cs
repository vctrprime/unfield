using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Commands.Rates.PriceGroups;
using StadiumEngine.Handlers.Facades.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Handlers.Rates.PriceGroups;

internal sealed class UpdatePriceGroupHandler : BaseCommandHandler<UpdatePriceGroupCommand, UpdatePriceGroupDto>
{
    private readonly IUpdatePriceGroupFacade _facade;

    public UpdatePriceGroupHandler(
        IUpdatePriceGroupFacade facade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<UpdatePriceGroupDto> HandleCommand( UpdatePriceGroupCommand request,
        CancellationToken cancellationToken ) =>
        await _facade.Update( request, _currentStadiumId, _userId );
}