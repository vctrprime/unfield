using Unfield.Common;
using Unfield.Common.Exceptions;
using Unfield.Domain;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Services.Core.Rates;
using Unfield.DTO.Rates.Tariffs;
using Unfield.Commands.Rates.Tariffs;

namespace Unfield.Handlers.Facades.Rates.Tariffs;

internal class UpdateTariffFacade : IUpdateTariffFacade
{
    private readonly ITariffCommandService _commandService;
    private readonly ITariffQueryService _queryService;

    public UpdateTariffFacade( ITariffQueryService queryService, ITariffCommandService commandService )
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    public async Task<UpdateTariffDto> UpdateAsync(
        UpdateTariffCommand request,
        List<PromoCode> promoCodes,
        int stadiumId,
        int userId )
    {
        Tariff? tariff = await _queryService.GetByTariffIdAsync( request.Id, stadiumId );

        if ( tariff == null )
        {
            throw new DomainException( ErrorsKeys.TariffNotFound );
        }

        tariff.Name = request.Name;
        tariff.Description = request.Description;
        tariff.IsActive = request.IsActive;
        tariff.UserModifiedId = userId;
        tariff.DateStart = request.DateStart;
        tariff.DateEnd = request.DateEnd;
        tariff.Monday = request.Monday;
        tariff.Tuesday = request.Tuesday;
        tariff.Wednesday = request.Wednesday;
        tariff.Thursday = request.Thursday;
        tariff.Friday = request.Friday;
        tariff.Saturday = request.Saturday;
        tariff.Sunday = request.Sunday;

        await _commandService.UpdateTariffAsync(
            tariff,
            request.DayIntervals.Select( x => x.Interval ).ToList(),
            promoCodes );

        return new UpdateTariffDto();
    }
}