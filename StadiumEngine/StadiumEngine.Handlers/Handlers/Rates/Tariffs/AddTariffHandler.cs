using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Handlers.Commands.Rates.Tariffs;

namespace StadiumEngine.Handlers.Handlers.Rates.Tariffs;

internal sealed class AddTariffHandler : BaseCommandHandler<AddTariffCommand, AddTariffDto>
{
    private readonly ITariffCommandFacade _tariffFacade;

    public AddTariffHandler(
        ITariffCommandFacade tariffFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _tariffFacade = tariffFacade;
    }

    protected override async ValueTask<AddTariffDto> HandleCommand( AddTariffCommand request,
        CancellationToken cancellationToken )
    {
        Tariff? tariff = Mapper.Map<Tariff>( request );
        tariff.StadiumId = _currentStadiumId;
        tariff.UserCreatedId = _userId;

        await _tariffFacade.AddTariff( tariff, request.DayIntervals, UnitOfWork );

        return await Task.Run( () => new AddTariffDto(), cancellationToken );
    }
}