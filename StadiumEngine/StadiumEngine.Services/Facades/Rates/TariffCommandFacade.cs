using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;

namespace StadiumEngine.Services.Facades.Rates;

internal class TariffCommandFacade : ITariffCommandFacade
{
    private readonly IDayIntervalRepository _dayIntervalRepository;
    private readonly ITariffDayIntervalRepository _tariffDayIntervalRepository;
    private readonly ITariffRepository _tariffRepository;

    public TariffCommandFacade( ITariffRepository tariffRepository, IDayIntervalRepository dayIntervalRepository,
        ITariffDayIntervalRepository tariffDayIntervalRepository )
    {
        _tariffRepository = tariffRepository;
        _dayIntervalRepository = dayIntervalRepository;
        _tariffDayIntervalRepository = tariffDayIntervalRepository;
    }

    public async Task AddTariff( Tariff tariff, List<string[]> intervals, IUnitOfWork unitOfWork )
    {
        _tariffRepository.Add( tariff );
        await unitOfWork.SaveChanges();

        await AddIntervals( tariff, intervals, unitOfWork );
    }

    public async Task UpdateTariff( Tariff tariff, List<string[]> intervals, IUnitOfWork unitOfWork )
    {
        _tariffRepository.Update( tariff );
        List<TariffDayInterval> intervalsToRemove = tariff.TariffDayIntervals
            .Where( k => !intervals.Exists( p => p[ 0 ] == k.DayInterval.Start && p[ 1 ] == k.DayInterval.End ) )
            .ToList();

        if ( intervalsToRemove.Any() )
        {
            _tariffDayIntervalRepository.Remove( intervalsToRemove );
        }

        List<TariffDayInterval> tariffDayIntervals = tariff.TariffDayIntervals.ToList();
        List<string[]> intervalsToAdd = intervals
            .Where(
                k => !tariffDayIntervals.Exists( x => x.DayInterval.Start == k[ 0 ] && x.DayInterval.End == k[ 1 ] ) )
            .ToList();

        await AddIntervals( tariff, intervalsToAdd, unitOfWork );
    }

    public async Task DeleteTariff( int tariffId, int stadiumId )
    {
        Tariff? tariff = await _tariffRepository.Get( tariffId, stadiumId );

        if ( tariff == null )
        {
            throw new DomainException( ErrorsKeys.TariffNotFound );
        }

        _tariffDayIntervalRepository.Remove( tariff.TariffDayIntervals );
        _tariffRepository.Remove( tariff );
    }

    private async Task AddIntervals( Tariff tariff, List<string[]> intervals, IUnitOfWork unitOfWork )
    {
        foreach ( string[] interval in intervals )
        {
            string start = interval[ 0 ];
            string end = interval[ 1 ];

            DayInterval? dayInterval = await _dayIntervalRepository.Get( start, end );
            if ( dayInterval is null )
            {
                dayInterval = new DayInterval
                {
                    Start = start,
                    End = end
                };
                _dayIntervalRepository.Add( dayInterval );
                await unitOfWork.SaveChanges();
            }

            _tariffDayIntervalRepository.Add(
                new TariffDayInterval
                {
                    DayIntervalId = dayInterval.Id,
                    TariffId = tariff.Id,
                    UserCreatedId = tariff.UserCreatedId
                } );
        }
    }
}