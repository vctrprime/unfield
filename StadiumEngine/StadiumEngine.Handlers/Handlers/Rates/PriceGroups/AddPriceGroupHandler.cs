using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Application.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.PriceGroups;
using StadiumEngine.Commands.Rates.PriceGroups;

namespace StadiumEngine.Handlers.Handlers.Rates.PriceGroups;

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