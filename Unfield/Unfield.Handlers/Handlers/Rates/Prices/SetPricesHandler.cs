using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Rates.Prices;
using Unfield.Commands.Rates.Prices;
using Unfield.Domain.Entities.Rates;
using Unfield.Handlers.Facades.Rates.Prices;

namespace Unfield.Handlers.Handlers.Rates.Prices;

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

    protected override async ValueTask<SetPricesDto> HandleCommandAsync( SetPricesCommand request,
        CancellationToken cancellationToken )
    {
        IEnumerable<Price>? prices = Mapper.Map<IEnumerable<Price>>( request.Prices );
        return await _facade.SetPricesAsync( prices, _currentStadiumId, _userId );
    }
        
}