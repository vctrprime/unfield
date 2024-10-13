using Unfield.Common.Enums.Bookings;
using Unfield.Common.Enums.Schedule;
using Unfield.DTO.Offers.Fields;
using Unfield.DTO.Schedule;

namespace Unfield.DTO.Customers;

/// <summary>
/// ДТО брони в списке заказчика
/// </summary>
public class CustomerBookingListItemDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер
    /// </summary>
    public string Number { get; set; } = null!;

    /// <summary>
    /// Источник
    /// </summary>
    public BookingSource Source { get; set; }

    /// <summary>
    /// День
    /// </summary>
    public DateTime Day { get; set; }

    /// <summary>
    /// Дата окончания еженедельной
    /// </summary>
    public DateTime? ClosedDay { get; set; }
    
    /// <summary>
    /// Тариф
    /// </summary>
    public string TariffName { get; set; } = null!;

    /// <summary>
    /// Площадка
    /// </summary>
    public FieldDto Field { get; set; }= null!;

    /// <summary>
    /// Еженедельное бронирование
    /// </summary>
    public bool IsWeekly  { get; set; }

    /// <summary>
    /// Раздевалка
    /// </summary>
    public string? LockerRoomName { get; set; }

    /// <summary>
    /// Промокод
    /// </summary>
    public string? PromoCode { get; set; }

    /// <summary>
    /// Скидка по промокоду
    /// </summary>
    public decimal? PromoValue { get; set; }

    /// <summary>
    /// Дополнительная скидка
    /// </summary>
    public decimal? ManualDiscount { get; set; }

    /// <summary>
    /// Общая сумма до скидок
    /// </summary>
    public decimal TotalAmountBeforeDiscount { get; set; }

    /// <summary>
    /// Общая сумма после скидок
    /// </summary>
    public decimal TotalAmountAfterDiscount { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public BookingStatus Status { get; set; }

    /// <summary>
    ///  Время начала
    /// </summary>
    public string Time { get; set; } = null!;

    /// <summary>
    /// Длительность
    /// </summary>
    public string Duration { get; set; }= null!;
    
    /// <summary>
    /// Начало события
    /// </summary>
    public DateTime Start { get; set; }
    
    /// <summary>
    /// Окончание события
    /// </summary>
    public DateTime End { get; set; }
}