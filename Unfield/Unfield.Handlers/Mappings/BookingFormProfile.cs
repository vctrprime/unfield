using AutoMapper;
using Unfield.Commands.BookingForm;
using Unfield.Common.Static;
using Unfield.Domain.Entities.Bookings;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Entities.Rates;
using Unfield.Domain.Entities.Settings;
using Unfield.Domain.Extensions;
using Unfield.Domain.Services.Models.BookingForm;
using Unfield.DTO.BookingForm;
using Unfield.DTO.Offers.Fields;
using Unfield.DTO.Offers.Inventories;
using Unfield.Services;

namespace Unfield.Handlers.Mappings;

internal class BookingFormProfile : Profile
{
    public BookingFormProfile()
    {
        CreateMap<BookingFormData, BookingFormDto>()
            .ForMember(
                dest => dest.Fields,
                act => act.MapFrom( s => MapBookingFormData( s ) ) );

        CreateMap<AddBookingDraftCommand, Booking>()
            .ForMember( dest => dest.Day, act => act.MapFrom( s => s.Day ) )
            .ForMember( dest => dest.StartHour, act => act.MapFrom( s => s.Hour ) )
            .ForMember( dest => dest.IsDraft, act => act.MapFrom( s => true ) )
            .ForMember( dest => dest.HoursCount, act => act.MapFrom( s => 1 ) );

        CreateMap<BookingCheckoutData, BookingCheckoutDto>()
            .ForMember(
                dest => dest.StadiumName,
                act => act.MapFrom( s => $"{s.Field.Stadium.Name}, {s.Field.Stadium.City.Name}" ) );
        CreateMap<BookingCheckoutDataDurationAmount, BookingCheckoutDurationAmountDto>();
        CreateMap<BookingCheckoutDataPointPrice, BookingCheckoutPointPriceDto>()
            .ForMember( dest => dest.End, act => act.Ignore() );
        CreateMap<BookingCheckoutDataDurationInventory, BookingCheckoutDurationInventoryDto>();
        CreateMap<BookingCheckoutDataInventory, BookingCheckoutInventoryDto>();


        CreateMap<FillBookingDataCommandCost, BookingCost>();
        CreateMap<FillBookingDataCommandInventory, BookingInventory>();
        CreateMap<FillBookingDataCommandCustomer, BookingCustomer>();
        CreateMap<FillBookingDataCommandPromo, BookingPromo>();
    }

    private List<BookingFormFieldDto> MapBookingFormData( BookingFormData bookingFormData )
    {
        IEnumerable<BookingFormFieldDto> bookingFormFields = bookingFormData.Fields.Select(
            x =>
            {
                List<BookingFormFieldSlotDto> bookingFormSlots = GetSlots(
                    x,
                    bookingFormData.Slots.ContainsKey( x.StadiumId )
                        ? bookingFormData.Slots[ x.StadiumId ]
                        : new List<(decimal, bool)>(),
                    bookingFormData.Prices,
                    bookingFormData.Day,
                    bookingFormData.Bookings );


                foreach ( BookingFormFieldSlotDto bookingFormSlot in bookingFormSlots )
                {
                    BookingFormFieldSlotDto? nextSlotAfterHour =
                        bookingFormSlots.FirstOrDefault(
                            s => s.Hour >= bookingFormSlot.Hour + 1 &&
                                 s.Enabled );

                    bookingFormSlot.Enabled = bookingFormSlot.Enabled && nextSlotAfterHour != null;
                    bookingFormSlot.DisabledByMinDuration =
                        bookingFormSlot.DisabledByMinDuration || nextSlotAfterHour == null;
                }

                return new BookingFormFieldDto
                {
                    Data = MapField( x ),
                    StadiumName =
                        bookingFormData.IsForCity ? $"{x.Stadium.Name}, {x.Stadium.Address}" : null,
                    Slots = bookingFormSlots.Take( bookingFormSlots.Count - 1 ).ToList()
                };
            } );

        return bookingFormFields.Where( x => x.Slots.Any() ).ToList();
    }

    private List<BookingFormFieldSlotDto> GetSlots(
        Field field,
        List<(decimal, bool)> slots,
        List<Price> prices,
        DateTime day,
        List<Booking> bookings )
    {
        List<BookingFormFieldSlotDto> result = new List<BookingFormFieldSlotDto>();
        foreach ( (decimal, bool) slot in slots )
        {
            List<BookingFormFieldSlotPriceDto> bookingFormPrices = GetPrices(
                field.Id,
                slot.Item1,
                prices,
                day,
                slots.Select( x => x.Item1 ).Max() );

            Booking? booking = FindBooking(
                bookings,
                field.Id,
                slot,
                ( decimal )0.5 );

            List<Break> breaks = field.BreakFields.Select( x => x.Break ).ToList();
            Break? fieldBreak = FindBreak(
                breaks,
                day,
                slot,
                ( decimal )0.5 );

            result.Add(
                new BookingFormFieldSlotDto
                {
                    Hour = slot.Item1,
                    Name = TimePointParser.Parse( slot.Item1 ),
                    Prices = bookingFormPrices,
                    Enabled = slot.Item2 && bookingFormPrices.Any() && booking == null && fieldBreak == null,
                    DisabledByMinDuration = bookingFormPrices.Any()
                                            && slot.Item2
                                            &&
                                            ( ( booking != null
                                                && booking.StartHour - ( decimal )0.5 == slot.Item1
                                                && FindBooking(
                                                    bookings,
                                                    field.Id,
                                                    slot,
                                                    0 ) == null ) ||
                                              (
                                                  fieldBreak != null
                                                  && fieldBreak.StartHour - ( decimal )0.5 == slot.Item1
                                                  && FindBreak(
                                                      breaks,
                                                      day,
                                                      slot,
                                                      0 ) == null
                                              ) )
                } );
        }

        return result;
    }

    private Booking? FindBooking(
        List<Booking> bookings,
        int fieldId,
        (decimal, bool) slot,
        decimal offset ) =>
        bookings.FirstOrDefault(
            x =>
                Predicates.RelatedBookingField( x, fieldId )
                && x.StartHour - offset <= slot.Item1 && x.StartHour + x.HoursCount > slot.Item1 );

    private Break? FindBreak(
        List<Break> breaks,
        DateTime day,
        (decimal, bool) slot,
        decimal offset ) =>
        breaks.FirstOrDefault(
            x => x.IsActive && !x.IsDeleted && x.DateStart.ToUniversalTime() <= day.ToUniversalTime() &&
                 ( !x.DateEnd.HasValue || x.DateEnd.Value.ToUniversalTime() >= day.ToUniversalTime() ) &&
                 x.StartHour - offset <= slot.Item1 && x.EndHour > slot.Item1 );

    private static List<BookingFormFieldSlotPriceDto> GetPrices(
        int fieldId,
        decimal slot,
        List<Price> prices,
        DateTime day,
        decimal lastSlot )
    {
        List<Price> slotPrices = prices.Where(
            x => x.FieldId == fieldId
                 && TimePointParser.Parse( x.TariffDayInterval.DayInterval.Start ) <= slot
                 && TimePointParser.Parse( x.TariffDayInterval.DayInterval.End ) >
                 ( slot == lastSlot ? lastSlot - ( decimal )0.1 : slot )
                 && x.TariffDayInterval.Tariff.HasDayOfWeek( day )
                 && x.TariffDayInterval.Tariff.DateStart.Date <= day.Date.ToUniversalTime()
                 && ( x.TariffDayInterval.Tariff.DateEnd?.Date ?? new DateTime( 3000, 1, 1 ) ).ToUniversalTime() >=
                 day.Date
                 && x.Value > 0 ).ToList();

        return slotPrices.Select(
            x => new BookingFormFieldSlotPriceDto
            {
                TariffId = x.TariffDayInterval.TariffId,
                TariffName = x.TariffDayInterval.Tariff.Name,
                Value = x.Value
            } ).ToList();
    }

    private FieldDto MapField( Field field ) =>
        new FieldDto
        {
            Id = field.Id,
            Name = field.Name,
            CoveringType = field.CoveringType,
            Description = field.Description,
            Images = field.Images.OrderBy( i => i.Order ).Select( i => i.Path ).ToList(),
            SportKinds = field.SportKinds.Select( k => k.SportKind ).ToList(),
            Width = field.Width,
            Length = field.Length
        };
}