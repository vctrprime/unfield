using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Services.Validators.Rates;

internal interface ITariffValidator
{
    Task ValidateAsync( int stadiumId, List<string[]> intervals, List<PromoCode> promoCodes );
}