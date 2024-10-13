using Unfield.Domain.Entities.Rates;

namespace Unfield.Services.Validators.Rates;

internal interface ITariffValidator
{
    Task ValidateAsync( int stadiumId, List<string[]> intervals, List<PromoCode> promoCodes );
}