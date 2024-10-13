using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Core.Rates;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Rates.Tariffs;
using Unfield.Commands.Rates.Tariffs;

namespace Unfield.Handlers.Handlers.Rates.Tariffs;

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