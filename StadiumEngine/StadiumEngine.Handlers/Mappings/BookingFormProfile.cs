using AutoMapper;
using StadiumEngine.Commands.BookingForm;
using StadiumEngine.Common.Enums.BookingForm;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Entities.BookingForm;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Entities.Rates;
using StadiumEngine.Domain.Extensions;
using StadiumEngine.Domain.Services.Models.BookingForm;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.DTO.Offers.Fields;

namespace StadiumEngine.Handlers.Mappings;

internal class BookingFormProfile : Profile
{
    public BookingFormProfile()
    {
        CreateMap<BookingFormData, BookingFormDto>()
            .ForMember(
                dest => dest.Fields,
                act => act.MapFrom( s => MapBookingFormData( s ) ) );

        CreateMap<AddBookingDraftCommand, Booking>()
            .ForMember( dest => dest.Day, act => act.MapFrom( s => s.Day.Date ) )
            .ForMember( dest => dest.StartHour, act => act.MapFrom( s => TimePointParser.Parse( s.Slot ) ) )
            .ForMember( dest => dest.IsDraft, act => act.MapFrom( s => true ) )
            .ForMember( dest => dest.HoursCount, act => act.MapFrom( s => 1 ) )
            .ForMember(
                dest => dest.Source,
                act => act.MapFrom( s => BookingSource.Form ) );
    }

    private List<BookingFormFieldDto> MapBookingFormData( BookingFormData bookingFormData )
    {
        IEnumerable<BookingFormFieldDto> bookingFormFields = bookingFormData.Fields.Select(
            x =>
            {
                List<BookingFormFieldSlotDto> bookingFormSlots = GetSlots(
                    x.Id,
                    bookingFormData.Slots[ x.StadiumId ],
                    bookingFormData.Prices,
                    bookingFormData.Day,
                    bookingFormData.Bookings );

                //убираем те слоты у которых до закрытия полчаса (бронирование минимум на час)
                List<BookingFormFieldSlotDto> resultSlots = ( from bookingFormSlot in bookingFormSlots
                    let nextSlotAfterHour =
                        bookingFormSlots.FirstOrDefault(
                            s => TimePointParser.Parse( s.Name ) >= TimePointParser.Parse( bookingFormSlot.Name ) + 1 )
                    where nextSlotAfterHour != null
                    select bookingFormSlot ).ToList();

                return new BookingFormFieldDto
                {
                    Data = MapField( x ),
                    StadiumName =
                        bookingFormData.IsForCity ? $"{x.Stadium.Name}, {x.Stadium.Address}" : null,
                    Slots = resultSlots
                };
            } );

        return bookingFormFields.Where( x => x.Slots.Any() ).ToList();
    }

    private List<BookingFormFieldSlotDto> GetSlots(
        int fieldId,
        List<decimal> slots,
        List<Price> prices,
        DateTime day,
        List<Booking> bookings ) =>
        ( from slot in slots
            let bookingFormPrices = GetPrices(
                fieldId,
                slot,
                prices,
                day,
                slots.Max() )
            //смотрим также на предудыщие полчаса от бронирований, на полчаса нельзя бронировать
            let booking = bookings.FirstOrDefault(
                x => x.FieldId == fieldId
                     && x.StartHour - ( decimal )0.5 <= slot
                     && x.StartHour + x.HoursCount > slot )
            where bookingFormPrices.Any() && booking == null
            select new BookingFormFieldSlotDto
            {
                Name = TimePointParser.Parse( slot ),
                Prices = bookingFormPrices
            } ).ToList();

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