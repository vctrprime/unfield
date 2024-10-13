using AutoMapper;
using Unfield.Domain;
using Unfield.Domain.Services.Identity;
using Unfield.DTO.Rates.Tariffs;
using Unfield.Commands.Rates.Tariffs;
using Unfield.Domain.Entities.Rates;
using Unfield.Handlers.Facades.Rates.Tariffs;

namespace Unfield.Handlers.Handlers.Rates.Tariffs;

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

    protected override async ValueTask<UpdateTariffDto> HandleCommandAsync( UpdateTariffCommand request,
        CancellationToken cancellationToken ) =>
        await _facade.UpdateAsync(
            request,
            Mapper.Map<List<PromoCode>>( request.PromoCodes ),
            _currentStadiumId,
            _userId );
}