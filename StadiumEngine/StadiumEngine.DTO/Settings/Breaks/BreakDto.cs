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
    public decimal StartHour { get; set; }
    
    /// <summary>
    /// Час окончания
    /// </summary>
    public decimal EndHour { get; set; }

    /// <summary>
    /// Площадки
    /// </summary>
    public List<int> SelectedFields { get; set; } = new List<int>();

    /// <summary>
    ///  Количество площадок
    /// </summary>
    public int FieldsCount => SelectedFields.Count;
}