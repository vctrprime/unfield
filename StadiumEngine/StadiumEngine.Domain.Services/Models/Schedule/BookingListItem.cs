using StadiumEngine.Common.Enums.BookingForm;

namespace StadiumEngine.Domain.Services.Models.Schedule;

public class BookingListItem
{
    public int Id { get; set; }
    public string Number { get; set; } = null!;
    public BookingSource Source { get; set; }
    public DateTime? Day { get; set; }
    public DateTime? ClosedDay { get; set; }
    public string Time { get; set; } = null!;
    public decimal HoursCount { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerPhoneNumber { get; set; }
    public string? TariffName { get; set; }
    public string? FieldName { get; set; }
    public bool IsWeekly { get; set; }
    public string? LockerRoomName { get; set; }
    public string? PromoCode { get; set; }
    public decimal? PromoValue { get; set; }
    public decimal? ManualDiscount { get; set; }
    public decimal TotalAmountBeforeDiscount { get; set; }
    public decimal TotalAmountAfterDiscount { get; set; }
}