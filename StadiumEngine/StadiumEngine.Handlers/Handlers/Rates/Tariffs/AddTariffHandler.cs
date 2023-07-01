using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Core.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Commands.Rates.Tariffs;

namespace StadiumEngine.Handlers.Handlers.Rates.Tariffs;

internal sealed class AddTariffHandler : BaseCommandHandler<AddTariffCommand, AddTariffDto>
{
    private readonly ITariffCommandService _commandService;

    public AddTariffHandler(
        ITariffCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<AddTariffDto> HandleCommandAsync( AddTariffCommand request,
        CancellationToken cancellationToken )
    {
        Tariff? tariff = Mapper.Map<Tariff>( request );
        tariff.StadiumId = _currentStadiumId;
        tariff.UserCreatedId = _userId;

        await _commandService.AddTariffAsync( tariff, request.DayIntervals.Select( x => x.Interval ).ToList() );

        return new AddTariffDto();
    }
}