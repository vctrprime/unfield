using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Repositories.Rates;

namespace StadiumEngine.Services.Facades.Rates;

public class TariffRepositoryFacade : ITariffRepositoryFacade
{
    private readonly IDayIntervalRepository _dayIntervalRepository;
    private readonly ITariffDayIntervalRepository _tariffDayIntervalRepository;
    private readonly ITariffRepository _tariffRepository;
    private readonly IPromoCodeRepository _promoCodeRepository;

    public TariffRepositoryFacade( IDayIntervalRepository dayIntervalRepository, ITariffDayIntervalRepository tariffDayIntervalRepository, ITariffRepository tariffRepository, IPromoCodeRepository promoCodeRepository )
    {
        _dayIntervalRepository = dayIntervalRepository;
        _tariffDayIntervalRepository = tariffDayIntervalRepository;
        _tariffRepository = tariffRepository;
        _promoCodeRepository = promoCodeRepository;
    }

    public void AddTariff( Tariff tariff ) => _tariffRepository.Add( tariff );
    
    public void UpdateTariff( Tariff tariff ) => _tariffRepository.Update( tariff );

    public async Task<Tariff?> GetTariffAsync( int tariffId, int stadiumId ) =>
        await _tariffRepository.GetAsync( tariffId, stadiumId );
    
    public void RemoveTariff( Tariff tariff ) => _tariffRepository.Remove( tariff );
    public void AddTariffDayInterval( TariffDayInterval tariffDayInterval ) => _tariffDayIntervalRepository.Add( tariffDayInterval );

    public void RemoveTariffDayIntervals( IEnumerable<TariffDayInterval> tariffDayIntervals ) =>
        _tariffDayIntervalRepository.Remove( tariffDayIntervals );

    public async Task<DayInterval?> GetDayIntervalAsync( string start, string end ) =>
        await _dayIntervalRepository.GetAsync( start, end );

    public void AddDayInterval( DayInterval dayInterval ) => _dayIntervalRepository.Add( dayInterval );

    public void AddPromoCode( PromoCode promoCode ) => _promoCodeRepository.Add( promoCode );

    public void UpdatePromoCode( PromoCode promoCode ) => _promoCodeRepository.Update( promoCode );

    public void RemovePromoCodes( IEnumerable<PromoCode> promoCodes ) => _promoCodeRepository.Remove( promoCodes );
}