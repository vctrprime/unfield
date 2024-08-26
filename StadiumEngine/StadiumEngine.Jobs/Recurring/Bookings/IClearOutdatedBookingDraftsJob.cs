namespace StadiumEngine.Jobs.Recurring.Bookings;

public interface IClearOutdatedBookingDraftsJob
{
    Task Clear();
}