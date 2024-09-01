using StadiumEngine.Domain.Entities.Bookings;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Models.BookingForm;

public class BookingFormData
{
    public int? StadiumId { get; set; }
    public bool IsForCity { get; set; }
    public DateTime Day { get; set; }
    public List<Field> Fields { get; set; } = new();
    public Dictionary<int, List<(decimal, bool)>> Slots { get; set; } = new();
    public List<Price> Prices { get; set; } = new();
    public List<Booking> Bookings { get; set; } = new();

}