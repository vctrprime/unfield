using StadiumEngine.Domain.Entities.Bookings;

namespace StadiumEngine.Services;

public static class Predicates
{
    public static Func<Booking, int, bool> RelatedBookingField = ( x, fieldId ) => x.FieldId == fieldId ||
        x.Field.ParentFieldId == fieldId ||
        x.Field.ChildFields.Any( cf => cf.Id == fieldId );
}