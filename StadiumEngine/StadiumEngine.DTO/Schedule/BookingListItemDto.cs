using StadiumEngine.Common.Enums.BookingForm;
using StadiumEngine.Common.Enums.Schedule;
using StadiumEngine.Common.Static;

namespace StadiumEngine.DTO.Schedule;

/// <summary>
/// ДТО брони в списке
/// </summary>
public class BookingListItemDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id => OriginalData.Id;

    /// <summary>
    /// Номер
    /// </summary>
    public string Number => OriginalData.Number;

    /// <summary>
    /// Источник
    /// </summary>
    public BookingSource Source => OriginalData.Source;

    /// <summary>
    /// День
    /// </summary>
    public DateTime? Day { get; set; }

    /// <summary>
    /// Дата окончания еженедельной
    /// </summary>
    public DateTime? ClosedDay { get; set; }

    /// <summary>
    /// Время начала
    /// </summary>
    public decimal StartHour  => OriginalData.StartHour;

    /// <summary>
    /// Общее количество часов
    /// </summary>
    public decimal HoursCount => OriginalData.HoursCount;

    /// <summary>
    /// Имя заказчика
    /// </summary>
    public string? CustomerName  => OriginalData.Customer.Name;

    /// <summary>
    /// Номер телефона заказчика
    /// </summary>
    public string? CustomerPhoneNumber  => OriginalData.Customer.PhoneNumber;

    /// <summary>
    /// Тариф
    /// </summary>
    public string TariffName  => OriginalData.Tariff.Name;

    /// <summary>
    /// Площадка
    /// </summary>
    public string FieldName  => OriginalData.Field.Name;

    /// <summary>
    /// Еженедельное бронирование
    /// </summary>
    public bool IsWeekly  => OriginalData.IsWeekly;

    /// <summary>
    /// Раздевалка
    /// </summary>
    public string? LockerRoomName  => OriginalData.LockerRoom?.Name;

    /// <summary>
    /// Промокод
    /// </summary>
    public string? PromoCode  => OriginalData.Promo?.Code;

    /// <summary>
    /// Скидка по промокоду
    /// </summary>
    public decimal? PromoValue  => OriginalData.Promo?.Value;

    /// <summary>
    /// Дополнительная скидка
    /// </summary>
    public decimal? ManualDiscount  => OriginalData.ManualDiscount;

    /// <summary>
    /// Общая сумма до скидок
    /// </summary>
    public decimal TotalAmountBeforeDiscount  => OriginalData.TotalAmountBeforeDiscount;

    /// <summary>
    /// Общая сумма после скидок
    /// </summary>
    public decimal TotalAmountAfterDiscount  => OriginalData.TotalAmountAfterDiscount;

    /// <summary>
    /// Статус
    /// </summary>
    public BookingStatus Status { get; set; }

    /// <summary>
    ///  Время начала (текст)
    /// </summary>
    public string Time => TimePointParser.Parse( HoursCount );

    /// <summary>
    /// Данные по брони
    /// </summary>
    public BookingDto OriginalData { get; set; } = null!;

    /// <summary>
    /// Начало события
    /// </summary>
    public DateTime Start => Day?.AddHours( ( double )StartHour ) ?? DateTime.Now;
}