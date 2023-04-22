using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;

namespace StadiumEngine.Domain.Services.Facades.BookingForm;

public interface IBookingFormQueryFacade
{
    Task<List<Field>> GetFieldsForBookingFormAsync( string? token, int? cityId, string? q );
    Task<Dictionary<int, List<int>>> GetSlotsAsync( List<int> stadiumsIds );
    Task<List<Price>> GetPrices( List<int> stadiumsIds );
}