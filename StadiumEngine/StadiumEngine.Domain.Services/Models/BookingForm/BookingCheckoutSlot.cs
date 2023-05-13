namespace StadiumEngine.Domain.Services.Models.BookingForm;

public class BookingCheckoutSlot
{
    public decimal Hour { get; set; }
    public List<BookingCheckoutSlotPrice> Prices { get; set; } = null!;
}

public class BookingCheckoutSlotPrice
{
    public int TariffId { get; set; }
    public decimal Value { get; set; }
}