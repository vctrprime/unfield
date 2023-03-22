using StadiumEngine.Common.Enums.Rates;

namespace StadiumEngine.DTO.Rates.Tariffs;

/// <summary>
///     ДТО тарифа
/// </summary>
public class TariffDto : BaseEntityDto
{
    /// <summary>
    ///     Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Название
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Активность
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    ///     Дата начала тарифа
    /// </summary>
    public DateTime DateStart { get; set; }

    /// <summary>
    ///     Дата окончания тарифа
    /// </summary>
    public DateTime? DateEnd { get; set; }

    /// <summary>
    ///     Действует в понедельник
    /// </summary>
    public bool Monday { get; set; }

    /// <summary>
    ///     Действует во вторник
    /// </summary>
    public bool Tuesday { get; set; }

    /// <summary>
    ///     Действует в среду
    /// </summary>
    public bool Wednesday { get; set; }

    /// <summary>
    ///     Действует в четверг
    /// </summary>
    public bool Thursday { get; set; }

    /// <summary>
    ///     Действует в пятницу
    /// </summary>
    public bool Friday { get; set; }

    /// <summary>
    ///     Действует в субботу
    /// </summary>
    public bool Saturday { get; set; }

    /// <summary>
    ///     Действует в воскресенье
    /// </summary>
    public bool Sunday { get; set; }

    /// <summary>
    ///     Временные интвервалы
    /// </summary>
    public List<TariffDayIntervalDto> DayIntervals { get; set; } = new();
    
    /// <summary>
    /// Промокоды
    /// </summary>
    public List<PromoCodeDto> PromoCodes { get; set; } = new();
}

/// <summary>
/// ДТО интервала в тарифе
/// </summary>
public class TariffDayIntervalDto
{
    /// <summary>
    /// ID связки
    /// </summary>
    public int TariffDayIntervalId { get; set; }
    
    /// <summary>
    /// Интервал
    /// </summary>
    public string[] Interval { get; set; }
}

/// <summary>
/// ДТО промокоды
/// </summary>
public class PromoCodeDto
{
    /// <summary>
    /// Код
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// Тип
    /// </summary>
    public PromoCodeType Type { get; set; }
    
    /// <summary>
    /// Значение скидки
    /// </summary>
    public decimal Value { get; set; }
}