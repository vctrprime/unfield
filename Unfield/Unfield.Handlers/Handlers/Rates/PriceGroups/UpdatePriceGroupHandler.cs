using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Rates.PriceGroups;
using Unfield.Commands.Rates.PriceGroups;
using Unfield.Handlers.Facades.Rates.PriceGroups;

namespace Unfield.Handlers.Handlers.Rates.PriceGroups;

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

    protected override async ValueTask<UpdatePriceGroupDto> HandleCommandAsync( UpdatePriceGroupCommand request,
        CancellationToken cancellationToken ) =>
        await _facade.UpdateAsync( request, _currentStadiumId, _userId );
}