using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.BookingForm;

public interface IBookingFormQueryFacade
{
    Task<List<Field>> GetFieldsForBookingFormAsync( string? token, int? cityId, string? q );
    Task<Dictionary<int, List<decimal>>> GetSlotsAsync( List<int> stadiumsIds );
    Task<List<Price>> GetPricesAsync( List<int> stadiumsIds );
    Task<List<Booking>> GetBookingsAsync( DateTime day, List<int> stadiumsIds );
}