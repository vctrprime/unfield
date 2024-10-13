using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Services.Core.Rates;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Rates.PriceGroups;
using Unfield.Commands.Rates.PriceGroups;

namespace Unfield.Handlers.Handlers.Rates.PriceGroups;

internal sealed class AddPriceGroupHandler : BaseCommandHandler<AddPriceGroupCommand, AddPriceGroupDto>
{
    private readonly IPriceGroupCommandService _commandService;

    public AddPriceGroupHandler(
        IPriceGroupCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<AddPriceGroupDto> HandleCommandAsync( AddPriceGroupCommand request,
        CancellationToken cancellationToken )
    {
        PriceGroup? priceGroup = Mapper.Map<PriceGroup>( request );
        priceGroup.StadiumId = _currentStadiumId;
        priceGroup.UserCreatedId = _userId;

        _commandService.AddPriceGroup( priceGroup );

        return await Task.Run( () => new AddPriceGroupDto(), cancellationToken );
    }
}