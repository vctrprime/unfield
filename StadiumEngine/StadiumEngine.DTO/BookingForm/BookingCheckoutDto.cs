using StadiumEngine.Common.Static;
using StadiumEngine.DTO.Customers;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.DTO.Rates.Tariffs;

namespace StadiumEngine.DTO.BookingForm;

/// <summary>
/// ДТО чекаута при бронировании
/// </summary>
public class BookingCheckoutDto
{
    /// <summary>
    /// Номер бронирования
    /// </summary>
    public string BookingNumber { get; set; } = null!;
    
    /// <summary>
    /// День
    /// </summary>
    public string Day { get; set; } = null!;
    
    /// <summary>
    /// Стадион
    /// </summary>
    public string StadiumName { get; set; } = null!;

    /// <summary>
    /// id тарифа
    /// </summary>
    public int TariffId { get; set; }
    
    /// <summary>
    /// Данные площадки
    /// </summary>
    public FieldDto Field { get; set; } = null!;
    
    /// <summary>
    /// Предлагаемый инвентарь
    /// </summary>
    public List<BookingCheckoutDurationInventoryDto> DurationInventories { get; set; } = null!;
    
    /// <summary>
    /// Стоимость в зависимости от длительности бронирования
    /// </summary>
    public List<BookingCheckoutDurationAmountDto> DurationAmounts { get; set; } = null!;
    
    /// <summary>
    /// Цена за каждые возможные полчаса в брони
    /// </summary>
    public List<BookingCheckoutPointPriceDto> PointPrices { get; set; } = null!;
    
    /// <summary>
    /// Авторизованный заказчик
    /// </summary>
    public AuthorizedCustomerDto? Customer { get; set; }
}

/// <summary>
/// Стоимость в зависимости от длительности бронирования
/// </summary>
public class BookingCheckoutDurationAmountDto
{
    /// <summary>
    /// Длительность
    /// </summary>
    public decimal Duration { get; set; }
    
    /// <summary>
    /// Значение
    /// </summary>
    public decimal Value { get; set; }
}

/// <summary>
/// Цена за каждые возможные полчаса в брони 
/// </summary>
public class BookingCheckoutPointPriceDto
{
    /// <summary>
    /// Начало интервала
    /// </summary>
    public decimal Start { get; set; }

    /// <summary>
    /// Начало интервала (строка)
    /// </summary>
    public string DisplayStart => TimePointParser.Parse( Start );

    /// <summary>
    /// Окончание интервала
    /// </summary>
    public decimal End => Start + ( decimal )0.5;
    
    /// <summary>
    /// Окончание интервала (строка)
    /// </summary>
    public string DisplayEnd => TimePointParser.Parse( End );
    
    /// <summary>
    /// Значение цены
    /// </summary>
    public decimal Value { get; set; }
}

/// <summary>
/// Инвентарь в зависимости от продолжительности
/// </summary>
public class BookingCheckoutDurationInventoryDto
{
    /// <summary>
    /// Длительность
    /// </summary>
    public decimal Duration { get; set; }
    
    /// <summary>
    /// Инвентарь
    /// </summary>
    public List<BookingCheckoutInventoryDto> Inventories { get; set; } = null!;
}

/// <summary>
/// Инвентарь в чекауте
/// </summary>
public class BookingCheckoutInventoryDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// Количество
    /// </summary>
    public decimal Quantity { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Изображение
    /// </summary>
    public string? Image { get; set; }
}