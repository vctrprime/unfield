using Unfield.Common.Enums.Rates;

namespace Unfield.Domain.Entities.Rates;

public class PromoCode : BaseUserEntity
{
    public string Code { get; set; }
    public PromoCodeType Type { get; set; }
    public decimal Value { get; set; }
    public int TariffId { get; set; }
    
    public virtual Tariff Tariff { get; set; }
}