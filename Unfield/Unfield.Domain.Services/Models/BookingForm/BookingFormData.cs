using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Entities.Rates;

namespace Unfield.Domain.Services.Models.BookingForm;

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