using Unfield.DTO.Customers;
using Unfield.DTO.Offers.Fields;

namespace Unfield.DTO.BookingForm;

/// <summary>
/// ДТО формы бронирования
/// </summary>
public class BookingFormDto
{
    /// <summary>
    /// Id стадиона по токену
    /// </summary>
    public int? StadiumId { get; set; }
    
    /// <summary>
    /// Площадки
    /// </summary>
    public List<BookingFormFieldDto> Fields { get; set; } = new();
    
    /// <summary>
    /// Авторизованный заказчик
    /// </summary>
    public AuthorizedCustomerDto? Customer { get; set; }
}

/// <summary>
/// Площадка на форме бронирования
/// </summary>
public class BookingFormFieldDto
{
    /// <summary>
    /// Данные площадки
    /// </summary>
    public FieldDto Data { get; set; } = null!;

    /// <summary>
    /// Минимальная цена
    /// </summary>
    public decimal MinPrice => Slots.SelectMany( x => x.Prices ).Min( p => p.Value );
    
    /// <summary>
    /// Название и адрес стадиона
    /// </summary>
    public string? StadiumName { get; set; }
    
    /// <summary>
    /// Свободные слоты
    /// </summary>
    public List<BookingFormFieldSlotDto> Slots { get; set; } = new();
}

/// <summary>
/// ДТО слота площадки
/// </summary>
public class BookingFormFieldSlotDto
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    ///  Час в числовом выражении
    /// </summary>
    public decimal Hour { get; set; }
    
    /// <summary>
    /// Доступен
    /// </summary>
    public bool Enabled { get; set; }
    
    /// <summary>
    /// Заблокрован условием, что минимальное бронирование = 1 час
    /// </summary>
    public bool DisabledByMinDuration { get; set; }
    
    /// <summary>
    /// Цены
    /// </summary>
    public List<BookingFormFieldSlotPriceDto> Prices { get; set; } = null!;
}

/// <summary>
/// ДТО цены по слоту
/// </summary>
public class BookingFormFieldSlotPriceDto
{
    /// <summary>
    /// Id тарифа
    /// </summary>
    public int TariffId { get; set; }
    
    /// <summary>
    /// Название тарифа
    /// </summary>
    public string TariffName { get; set; } = null!;

    /// <summary>
    /// Цена
    /// </summary>
    public decimal Value { get; set; }
}
