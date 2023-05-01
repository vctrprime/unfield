using StadiumEngine.Domain.Entities.Offers;

namespace StadiumEngine.Services.Facades.Services.BookingForm;

internal interface IBookingFormFieldRepositoryFacade
{
    Task<List<Field>> GetFieldsForBookingFormAsync( string? token, int? cityId, string? q );
}