using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Domain.Services.Facades.BookingForm;

public interface IBookingFormQueryFacade
{
    Task<List<Field>> GetFieldsForBookingFormAsync( string? token, int? cityId, string? q );
}