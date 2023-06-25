using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Rates;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Entities.Settings;
using StadiumEngine.Domain.Repositories.Settings;

namespace StadiumEngine.Services.Validators.Rates;

internal class TariffValidator : ITariffValidator
{
    private readonly IMainSettingsRepository _repository;

    public TariffValidator( IMainSettingsRepository repository )
    {
        _repository = repository;
    }

    public async Task ValidateAsync( int stadiumId, List<string[]> intervals, List<PromoCode> promoCodes )
    {
        ValidatePromoCodes( promoCodes );
        await ValidateIntervalsAsync( stadiumId, intervals );
    }

    private async Task ValidateIntervalsAsync( int stadiumId, List<string[]> intervals )
    {
        MainSettings mainSettings = await _repository.GetAsync( stadiumId );
        List<string> points = new();
        for ( int i = mainSettings.OpenTime; i <= mainSettings.CloseTime; i++ )
        {
            points.Add( i.ToString().Length == 1 ? $"0{i}:00" : $"{i}:00" );
            if ( i < mainSettings.CloseTime )
            {
                points.Add( i.ToString().Length == 1 ? $"0{i}:30" : $"{i}:30" );
            }
        }

        Dictionary<int, int> indexes = new();
        int? lastEndIndex = null;
        foreach ( string[] interval in intervals )
        {
            int startIndex = points.IndexOf( interval[ 0 ] );
            int endIndex = points.IndexOf( interval[ 1 ] );

            lastEndIndex ??= endIndex;

            for ( int i = startIndex; i <= endIndex; i++ )
            {
                if ( startIndex == lastEndIndex )
                {
                    lastEndIndex = endIndex;
                    continue;
                }

                if ( indexes.ContainsKey( i ) )
                {
                    indexes[ i ] += 1;
                }
                else
                {
                    indexes[ i ] = 1;
                }
            }
        }

        if ( indexes.Keys.Any( a => indexes[ a ] > 1 ) )
        {
            throw new DomainException( ErrorsKeys.CrossIntervals );
        }
    }

    private void ValidatePromoCodes( List<PromoCode> promoCodes )
    {
        foreach ( PromoCode promoCode in promoCodes )
        {
            if ( promoCode.Code.Length < 3 )
            {
                throw new DomainException( ErrorsKeys.PromoCodeMinLength );
            }

            if ( promoCode.Value <= 0 )
            {
                throw new DomainException( ErrorsKeys.PromoCodeMinValue );
            }

            if ( promoCode.Type == PromoCodeType.Percent && promoCode.Value > 99 )
            {
                throw new DomainException( ErrorsKeys.PromoCodeMaxValue );
            }

            PromoCode? sameCode = promoCodes.FirstOrDefault(
                c => c != promoCode && String.Equals(
                    c.Code,
                    promoCode.Code,
                    StringComparison.CurrentCultureIgnoreCase ) );
            if ( sameCode != null )
            {
                throw new DomainException( ErrorsKeys.PromoCodeSameCode );
            }
        }
    }
}