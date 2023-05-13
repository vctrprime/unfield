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
    /// Данные тарифа
    /// </summary>
    public TariffDto Tariff { get; set; } = null!;
    
    /// <summary>
    /// Данные площадки
    /// </summary>
    public FieldDto Field { get; set; } = null!;
    
    /// <summary>
    /// Предлагаемый инвентарь
    /// </summary>
    public List<InventoryDto> Inventories { get; set; } = null!;
    
    /// <summary>
    /// Стоимость в зависимости от длительности бронирования
    /// </summary>
    public List<BookingCheckoutDurationAmountDto> DurationAmounts { get; set; } = null!;
    
    /// <summary>
    /// Цена за каждые возможные полчаса в брони
    /// </summary>
    public List<BookingCheckoutPointPriceDto> PointPrices { get; set; } = null!;
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
    /// Окончание интервала
    /// </summary>
    public decimal End => Start + ( decimal )0.5;
    
    /// <summary>
    /// Значение цены
    /// </summary>
    public decimal Value { get; set; }
}