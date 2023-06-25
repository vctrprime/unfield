using StadiumEngine.DTO.Offers.Fields;

namespace StadiumEngine.DTO.Settings.Breaks;

/// <summary>
/// ДТО перерыва
/// </summary>
public class BreakDto : BaseEntityDto
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
    ///     Дата начала
    /// </summary>
    public DateTime DateStart { get; set; }

    /// <summary>
    ///     Дата окончания
    /// </summary>
    public DateTime? DateEnd { get; set; }

    /// <summary>
    /// Час начала
    /// </summary>
    public string StartHour { get; set; } = null!;
    
    /// <summary>
    /// Час окончания
    /// </summary>
    public string EndHour { get; set; } = null!;

    /// <summary>
    /// Площадки
    /// </summary>
    public List<FieldDto> Fields { get; set; } = new List<FieldDto>();

    /// <summary>
    ///  Количество площадок
    /// </summary>
    public int FieldsCount => Fields.Count;
}