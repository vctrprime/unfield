using AutoMapper;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Services.Application.Rates;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Commands.Rates.Tariffs;

namespace StadiumEngine.Handlers.Handlers.Rates.Tariffs;

internal sealed class DeleteTariffHandler : BaseCommandHandler<DeleteTariffCommand, DeleteTariffDto>
{
    private readonly ITariffCommandService _commandService;

    public DeleteTariffHandler(
        ITariffCommandService commandService,
        IMapper mapper,
        IClaimsIdentityService claimsIdentityService,
        IUnitOfWork unitOfWork ) : base( mapper, claimsIdentityService, unitOfWork )
    {
        _commandService = commandService;
    }

    protected override async ValueTask<DeleteTariffDto> HandleCommandAsync( DeleteTariffCommand request,
        CancellationToken cancellationToken )
    {
        await _commandService.DeleteTariffAsync( request.TariffId, _currentStadiumId );
        return new DeleteTariffDto();
    }
}