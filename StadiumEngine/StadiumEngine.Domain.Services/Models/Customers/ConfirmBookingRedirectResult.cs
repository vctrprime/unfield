using StadiumEngine.Domain.Entities.Customers;

namespace StadiumEngine.Domain.Services.Models.Customers;

public class ConfirmBookingRedirectResult
{
    public Customer Customer { get; set; } = null!;
    public string BookingNumber { get; set; } = null!;
    public int BookingStadiumId { get; set; }
}