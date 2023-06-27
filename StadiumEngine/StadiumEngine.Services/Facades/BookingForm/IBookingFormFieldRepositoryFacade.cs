using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Services.Facades.BookingForm;

internal interface IBookingFormFieldRepositoryFacade
{
    Task<List<Field>> GetFieldsForBookingFormAsync( string? token, int? cityId, string? q, int? fieldId );
}