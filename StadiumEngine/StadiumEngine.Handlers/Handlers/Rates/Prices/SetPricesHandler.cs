using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.Prices;
using StadiumEngine.Commands.Rates.Prices;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Handlers.Facades.Rates.Prices;

namespace StadiumEngine.Handlers.Handlers.Rates.Prices;

internal sealed class SetPricesHandler : BaseCommandHandler<SetPricesCommand, SetPricesDto>
{
    private readonly ISetPricesFacade _facade;

    public SetPricesHandler(
        ISetPricesFacade facade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<SetPricesDto> HandleCommand( SetPricesCommand request,
        CancellationToken cancellationToken )
    {
        IEnumerable<Price>? prices = Mapper.Map<IEnumerable<Price>>( request.Prices );
        return await _facade.SetPrices( prices, _currentStadiumId, _userId );
    }
        
}