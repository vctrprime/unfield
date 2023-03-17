using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.DTO.Rates.Tariffs;
using StadiumEngine.Commands.Rates.Tariffs;

namespace StadiumEngine.Handlers.Facades.Rates.Tariffs;

internal class UpdateTariffFacade : IUpdateTariffFacade
{
    private readonly ITariffCommandFacade _commandFacade;
    private readonly ITariffQueryFacade _queryFacade;

    public UpdateTariffFacade( ITariffQueryFacade queryFacade, ITariffCommandFacade commandFacade )
    {
        _queryFacade = queryFacade;
        _commandFacade = commandFacade;
    }

    public async Task<UpdateTariffDto> Update(
        UpdateTariffCommand request,
        int stadiumId,
        int userId,
        IUnitOfWork unitOfWork )
    {
        Tariff? tariff = await _queryFacade.GetByTariffId( request.Id, stadiumId );

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

        await _commandFacade.UpdateTariff( tariff, request.DayIntervals.Select( x => x.Interval ).ToList(), unitOfWork );

        return new UpdateTariffDto();
    }
}