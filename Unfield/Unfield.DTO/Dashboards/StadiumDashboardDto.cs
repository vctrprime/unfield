namespace Unfield.DTO.Dashboards;

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
    /// Средний чек
    /// </summary>
    public StadiumDashboardAverageBillDto AverageBill { get; set; }
    
    /// <summary>
    /// Распределение по площадкам
    /// </summary>
    public List<StadiumDashboardChartItemDto> FieldDistribution { get; set; }
    
    /// <summary>
    /// Популярность инвентаря
    /// </summary>
    public List<StadiumDashboardChartItemDto> PopularInventory { get; set; }
    
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

/// <summary>
/// Данные по среднему чеку
/// </summary>
public class StadiumDashboardAverageBillDto
{
    /// <summary>
    /// Средний чек по площадкам
    /// </summary>
    public decimal FieldValue { get; set; }
    
    /// <summary>
    /// Средний чек по инвентарю
    /// </summary>
    public decimal InventoryValue { get; set; }
    
    /// <summary>
    /// Средний чек общий
    /// </summary>
    public decimal TotalValue { get; set; }
}

