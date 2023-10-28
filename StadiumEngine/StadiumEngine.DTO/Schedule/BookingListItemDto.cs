using StadiumEngine.Common.Enums.BookingForm;

namespace StadiumEngine.DTO.Schedule;

/// <summary>
/// ДТО брони в списке
/// </summary>
public class BookingListItemDto
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
    /// Время начала
    /// </summary>
    public string Time { get; set; } = null!;
    
    /// <summary>
    /// Общее количество часов
    /// </summary>
    public decimal HoursCount { get; set; }
    
    /// <summary>
    /// Имя заказчика
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// Номер телефона заказчика
    /// </summary>
    public string? CustomerPhoneNumber { get; set; }
    
    /// <summary>
    /// Тариф
    /// </summary>
    public string? TariffName { get; set; }
    
    /// <summary>
    /// Площадка
    /// </summary>
    public string? FieldName { get; set; }
    
    /// <summary>
    /// Еженедельное бронирование
    /// </summary>
    public bool IsWeekly { get; set; }
    
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
}