using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Handlers.Commands.Rates.Tariffs;

namespace StadiumEngine.Handlers.Handlers.Rates.Tariffs;

internal sealed class DeleteTariffHandler : BaseCommandHandler<DeleteTariffCommand, DeleteTariffDto>
{
    private readonly ITariffCommandFacade _tariffFacade;

    public DeleteTariffHandler(
        ITariffCommandFacade tariffFacade,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _tariffFacade = tariffFacade;
    }

    protected override async ValueTask<DeleteTariffDto> HandleCommand( DeleteTariffCommand request,
        CancellationToken cancellationToken )
    {
        await _tariffFacade.DeleteTariff( request.TariffId, _currentStadiumId );
        return new DeleteTariffDto();
    }
}