using Unfield.Domain.Entities.Customers;

namespace Unfield.Domain.Services.Models.Customers;

public class ConfirmBookingRedirectResult
{
    public Customer Customer { get; set; } = null!;
    public string BookingNumber { get; set; } = null!;
    public string BookingStadiumToken { get; set; } = null!;
}