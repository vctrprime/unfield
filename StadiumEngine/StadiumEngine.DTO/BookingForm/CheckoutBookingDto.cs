using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.DTO.Offers.Inventories;
using StadiumEngine.DTO.Rates.Tariffs;

namespace StadiumEngine.DTO.BookingForm;

/// <summary>
/// ДТО чекаута при бронировании
/// </summary>
public class CheckoutBookingDto
{
    /// <summary>
    /// Номер бронирования
    /// </summary>
    public string BookingNumber { get; set; } = null!;
    
    
    /// <summary>
    /// Данные площадки
    /// </summary>
    public FieldDto Field { get; set; } = null!;
    
    /// <summary>
    /// Предлагаемы инвентарь
    /// </summary>
    public List<InventoryDto> Inventories { get; set; } = null!;
}
