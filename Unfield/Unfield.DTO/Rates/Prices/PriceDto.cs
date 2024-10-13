namespace Unfield.DTO.Rates.Prices;

/// <summary>
/// ДТО цены
/// </summary>
public class PriceDto
{
    /// <summary>
    /// Id площадки
    /// </summary>
    public int FieldId { get; set; }
    
    /// <summary>
    /// ID интервала
    /// </summary>
    public int TariffDayIntervalId { get; set; }

    /// <summary>
    /// Значение
    /// </summary>
    public decimal Value { get; set; }
}