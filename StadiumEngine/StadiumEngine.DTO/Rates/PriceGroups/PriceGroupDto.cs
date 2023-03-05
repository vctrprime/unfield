namespace StadiumEngine.DTO.Rates.PriceGroups;

/// <summary>
///     ДТО ценовой группы
/// </summary>
public class PriceGroupDto : BaseEntityDto
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
    ///     Площадки
    /// </summary>
    public List<string>? FieldNames { get; set; }
}