using StadiumEngine.Domain.Services.Models.BookingForm;

namespace StadiumEngine.Domain.Services.Application.BookingForm;

public interface IBookingFormQueryService
{
    Task<BookingFormData> GetBookingFormDataAsync( string? token, int? cityId, int? fieldId, string? q, DateTime day, int currentHour );
}