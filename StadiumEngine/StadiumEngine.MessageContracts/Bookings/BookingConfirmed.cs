namespace StadiumEngine.MessageContracts.Bookings
{
    public class BookingConfirmed
    {
        public string Number { get; }

        public BookingConfirmed( string number )
        {
            Number = number;
        }
    }
}