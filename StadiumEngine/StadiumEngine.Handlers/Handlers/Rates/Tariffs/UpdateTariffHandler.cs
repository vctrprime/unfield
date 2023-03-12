using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Commands.Rates.Tariffs;
using StadiumEngine.Handlers.Facades.Rates.Tariffs;

namespace StadiumEngine.Handlers.Handlers.Rates.Tariffs;

internal sealed class UpdateTariffHandler : BaseCommandHandler<UpdateTariffCommand, UpdateTariffDto>
{
    private readonly IUpdateTariffFacade _facade;

    public UpdateTariffHandler(
        IUpdateTariffFacade facade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _facade = facade;
    }

    protected override async ValueTask<UpdateTariffDto> HandleCommand( UpdateTariffCommand request,
        CancellationToken cancellationToken ) =>
        await _facade.Update(
            request,
            _currentStadiumId,
            _userId,
            UnitOfWork );
}