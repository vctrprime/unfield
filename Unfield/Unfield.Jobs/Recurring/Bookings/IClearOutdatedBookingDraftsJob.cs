namespace Unfield.Jobs.Recurring.Bookings;

public interface IClearOutdatedBookingDraftsJob
{
    Task Clear();
}