namespace StadiumEngine.DTO.Dashboards;

/// <summary>
/// ДТО дашборда
/// </summary>
public class StadiumDashboardDto
{
    /// <summary>
    /// Дата расчета
    /// </summary>
    public DateTime CalculationDate { get; set; }
    
    /// <summary>
    /// Количество брноей за 12 месяцев
    /// </summary>
    public List<StadiumDashboardChartItemDto> YearChart { get; set; }
    
    /// <summary>
    /// Распределение по времени бронирования
    /// </summary>
    public List<StadiumDashboardChartItemDto> TimeChart { get; set; }
}

/// <summary>
/// Элемент графика
/// </summary>
public class StadiumDashboardChartItemDto 
{
    /// <summary>
    /// Категория
    /// </summary>
    public string Category { get; set; }
    
    /// <summary>
    /// Значение
    /// </summary>
    public decimal Value { get; set; }
}

