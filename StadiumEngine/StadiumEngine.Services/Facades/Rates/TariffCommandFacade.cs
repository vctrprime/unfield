using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Services.Facades.Rates;
using StadiumEngine.Services.Facades.Services.Rates;
using StadiumEngine.Services.Validators.Rates;

namespace StadiumEngine.Services.Facades.Rates;

internal class TariffCommandFacade : ITariffCommandFacade
{
    private readonly ITariffRepositoryFacade _tariffRepositoryFacade;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITariffValidator _validator;

    public TariffCommandFacade( ITariffRepositoryFacade tariffRepositoryFacade, IUnitOfWork unitOfWork, ITariffValidator validator )
    {
        _tariffRepositoryFacade = tariffRepositoryFacade;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task AddTariffAsync( Tariff tariff, List<string[]> intervals )
    {
        await _validator.ValidateAsync( tariff.StadiumId, intervals, tariff.PromoCodes.ToList() );
        
        _tariffRepositoryFacade.AddTariff( tariff );
        await _unitOfWork.SaveChangesAsync();
        await AddIntervalsAsync( tariff, intervals );
    }

    public async Task UpdateTariffAsync( Tariff tariff, List<string[]> intervals, List<PromoCode> promoCodes )
    {
        await _validator.ValidateAsync( tariff.StadiumId, intervals, promoCodes );
        
        _tariffRepositoryFacade.UpdateTariff( tariff );
        await ProcessIntervalsAsync( tariff, intervals );
        ProcessPromoCodes( tariff, promoCodes );
    }

    public async Task DeleteTariffAsync( int tariffId, int stadiumId )
    {
        Tariff? tariff = await _tariffRepositoryFacade.GetTariffAsync( tariffId, stadiumId );

        if ( tariff == null )
        {
            throw new DomainException( ErrorsKeys.TariffNotFound );
        }

        _tariffRepositoryFacade.RemoveTariffDayIntervals( tariff.TariffDayIntervals );
        _tariffRepositoryFacade.RemovePromoCodes( tariff.PromoCodes );
        _tariffRepositoryFacade.RemoveTariff( tariff );
    }

    private async Task ProcessIntervalsAsync( Tariff tariff, List<string[]> intervals )
    {
        List<TariffDayInterval> intervalsToRemove = tariff.TariffDayIntervals
            .Where( k => !intervals.Exists( p => p[ 0 ] == k.DayInterval.Start && p[ 1 ] == k.DayInterval.End ) )
            .ToList();

        if ( intervalsToRemove.Any() )
        {
            _tariffRepositoryFacade.RemoveTariffDayIntervals( intervalsToRemove );
        }

        List<TariffDayInterval> tariffDayIntervals = tariff.TariffDayIntervals.ToList();
        List<string[]> intervalsToAdd = intervals
            .Where(
                k => !tariffDayIntervals.Exists( x => x.DayInterval.Start == k[ 0 ] && x.DayInterval.End == k[ 1 ] ) )
            .ToList();

        await AddIntervalsAsync( tariff, intervalsToAdd );
    }

    private void ProcessPromoCodes( Tariff tariff, List<PromoCode> promoCodes )
    {
        promoCodes.ForEach( p => p.TariffId = tariff.Id );
        List<PromoCode> promoCodesToRemove = tariff.PromoCodes
            .Where( x => !promoCodes.Exists( y => x.Code == y.Code && x.TariffId == y.TariffId ) )
            .ToList();

        if ( promoCodesToRemove.Any() )
        {
            _tariffRepositoryFacade.RemovePromoCodes( promoCodesToRemove );
        }

        foreach ( PromoCode promoCode in promoCodes )
        {
            PromoCode? entityPromoCode =
                tariff.PromoCodes.FirstOrDefault( x => x.Code == promoCode.Code && x.TariffId == promoCode.TariffId );
            if ( entityPromoCode == null )
            {
                promoCode.UserCreatedId = tariff.UserModifiedId;
                _tariffRepositoryFacade.AddPromoCode( promoCode );
            }
            else
            {
                entityPromoCode.Code = promoCode.Code;
                entityPromoCode.Type = promoCode.Type;
                entityPromoCode.Value = promoCode.Value;
                entityPromoCode.UserModifiedId = tariff.UserModifiedId;

                _tariffRepositoryFacade.UpdatePromoCode( entityPromoCode );
            }
        }
    }

    private async Task AddIntervalsAsync( Tariff tariff, List<string[]> intervals )
    {
        foreach ( string[] interval in intervals )
        {
            string start = interval[ 0 ];
            string end = interval[ 1 ];

            DayInterval? dayInterval = await _tariffRepositoryFacade.GetDayIntervalAsync( start, end );
            if ( dayInterval is null )
            {
                dayInterval = new DayInterval
                {
                    Start = start,
                    End = end
                };
                _tariffRepositoryFacade.AddDayInterval( dayInterval );
                await _unitOfWork.SaveChangesAsync();
            }

            _tariffRepositoryFacade.AddTariffDayInterval(
                new TariffDayInterval
                {
                    DayIntervalId = dayInterval.Id,
                    TariffId = tariff.Id,
                    UserCreatedId = tariff.UserCreatedId
                } );
        }
    }
}