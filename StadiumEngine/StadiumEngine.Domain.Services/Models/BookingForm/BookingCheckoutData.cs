using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Models.BookingForm;

public class BookingCheckoutData
{
    public string BookingNumber { get; set; } = null!;
    public string Day { get; set; } = null!;
    public Field Field { get; set; } = null!;
    public Tariff Tariff { get; set; } = null!;
    public Dictionary<Inventory, decimal> Inventories { get; set; } = null!;

    public List<BookingCheckoutDataDurationAmount> DurationAmounts { get; set; } = null!;
    
    public List<BookingCheckoutDataPointPrice> PointPrices { get; set; } = null!;
}

public class BookingCheckoutDataDurationAmount
{
    public decimal Duration { get; set; }
    public decimal Value { get; set; }
}

public class BookingCheckoutDataPointPrice
{
    public decimal Start { get; set; }
    public decimal End => Start + ( decimal )0.5;
    public decimal Value { get; set; }
}