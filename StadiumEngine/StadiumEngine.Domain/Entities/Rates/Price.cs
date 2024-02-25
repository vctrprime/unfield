using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Entities.Rates;

public class Price : BaseUserEntity
{
    public int FieldId { get; set; }
    public virtual Field Field { get; set; }
    public int TariffDayIntervalId { get; set; }
    public virtual TariffDayInterval TariffDayInterval { get; set; }
    public bool IsObsolete { get; set; }
    public Currency Currency { get; set; }
    public decimal Value { get; set; }
}