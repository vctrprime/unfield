using StadiumEngine.Common.Enums.BookingForm;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.DTO.Offers.LockerRooms;
using StadiumEngine.DTO.Rates.Tariffs;

namespace StadiumEngine.DTO.Schedule;

/// <summary>
/// ДТО брони
/// </summary>
public class BookingDto
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
    /// Сумма
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Время начала
    /// </summary>
    public decimal StartHour { get; set; }
    
    /// <summary>
    /// Общее количество часов
    /// </summary>
    public decimal HoursCount { get; set; }
    
    /// <summary>
    /// Черновик
    /// </summary>
    public bool IsDraft { get; set; }
    
    /// <summary>
    /// Подтверждена
    /// </summary>
    public bool IsConfirmed { get; set; }
    
    /// <summary>
    /// Отменена
    /// </summary>
    public bool IsCanceled { get; set; }
    
    /// <summary>
    /// Промокод
    /// </summary>
    public string? PromoCode { get; set; }
    
    /// <summary>
    /// Скидка
    /// </summary>
    public decimal? Discount { get; set; }
    
    /// <summary>
    /// Примечание
    /// </summary>
    public string? Note { get; set; }
    
    /// <summary>
    /// Поле
    /// </summary>
    public FieldDto Field { get; set; } = null!;
    
    /// <summary>
    /// Тариф
    /// </summary>
    public TariffDto Tariff { get; set; } = null!;
    
    /// <summary>
    /// Разевалка
    /// </summary>
    public LockerRoomDto? LockerRoom { get; set; }
    
    /// <summary>
    /// Заказчик
    /// </summary>
    public BookingCustomerDto Customer { get; set; } = null!;
    
    /// <summary>
    /// Цена по периодам
    /// </summary>
    public List<BookingCostDto> Costs { get; set; } = null!;
    
    /// <summary>
    /// Инвентарь
    /// </summary>
    public List<BookingInventoryDto> Inventories { get; set; } = null!;
}

/// <summary>
/// Заказчик
/// </summary>
public class BookingCustomerDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? PhoneNumber { get; set; }
}

/// <summary>
/// Стоимость по периодам
/// </summary>
public class BookingCostDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Начало периода
    /// </summary>
    public decimal StartHour { get; set; }
    
    /// <summary>
    /// Конец периода
    /// </summary>
    public decimal EndHour { get; set; }
    
    /// <summary>
    /// СТоимость
    /// </summary>
    public decimal Cost { get; set; }
}

/// <summary>
/// Инвентарь
/// </summary>
public class BookingInventoryDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Количество
    /// </summary>
    public decimal Quantity { get; set; }
    
    /// <summary>
    /// Стоимость
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Данные инвентаря
    /// </summary>
    public InventoryDto Inventory { get; set; } = null!;
}