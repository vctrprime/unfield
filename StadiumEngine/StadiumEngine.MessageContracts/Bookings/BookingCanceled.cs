using System;

namespace StadiumEngine.MessageContracts.Bookings
{
    public class BookingCanceled
    {
        public string Number { get; }
        public DateTime Day { get; }
        public string Reason { get; }

        public BookingCanceled( string number, DateTime day, string reason )
        {
            Number = number;
            Day = day;
            Reason = reason;
        }
    }
}