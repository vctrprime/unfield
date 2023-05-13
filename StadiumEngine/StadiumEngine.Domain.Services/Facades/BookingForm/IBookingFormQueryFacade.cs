using StadiumEngine.Domain.Services.Models.BookingForm;

namespace StadiumEngine.Domain.Services.Facades.BookingForm;

public interface IBookingFormQueryFacade
{
    Task<BookingFormData> GetBookingFormDataAsync( string? token, int? cityId, int? fieldId, string? q, DateTime day, int currentHour );
}