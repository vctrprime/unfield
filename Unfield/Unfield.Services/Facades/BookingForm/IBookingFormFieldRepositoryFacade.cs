using Unfield.Domain.Entities.Offers;

namespace Unfield.Services.Facades.BookingForm;

internal interface IBookingFormFieldRepositoryFacade
{
    Task<List<Field>> GetFieldsForBookingFormAsync( int? stadiumId, int? cityId, string? q, int? fieldId );
}