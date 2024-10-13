namespace Unfield.Queries.BookingForm;

public class GetBookingFormForMoveQuery : GetBookingFormQuery
{
    public string BookingNumber { get; set; } = null!;
}