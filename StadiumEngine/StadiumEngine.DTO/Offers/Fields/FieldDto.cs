using StadiumEngine.Common.Enums.Offers;

namespace StadiumEngine.DTO.Offers.Fields;

/// <summary>
///     ДТО площадки
/// </summary>
public class FieldDto : BaseEntityDto
{
    /// <summary>
    ///     Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Описание
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Изображения
    /// </summary>
    public List<string> Images { get; set; }

    /// <summary>
    ///     Ширина
    /// </summary>
    public decimal Width { get; set; }

    /// <summary>
    ///     Длина
    /// </summary>
    public decimal Length { get; set; }

    /// <summary>
    ///     Id родительской площадки
    /// </summary>
    public int? ParentFieldId { get; set; }

    /// <summary>
    ///     Тип покрытия
    /// </summary>
    public CoveringType CoveringType { get; set; }

    /// <summary>
    ///     Список видов спорта
    /// </summary>
    public List<SportKind> SportKinds { get; set; }

    /// <summary>
    ///     Активность
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    ///     Последний потомок в группе
    /// </summary>
    public bool IsLastChild { get; set; }

    /// <summary>
    ///     Потомки
    /// </summary>
    public List<string> ChildNames { get; set; }

    /// <summary>
    ///     Ценовая группа (id)
    /// </summary>
    public int? PriceGroupId { get; set; }

    /// <summary>
    ///     Ценовая группа (название)
    /// </summary>
    public string? PriceGroupName { get; set; }
}