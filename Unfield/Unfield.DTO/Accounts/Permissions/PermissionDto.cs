namespace Unfield.DTO.Accounts.Permissions;

/// <summary>
///     ДТО разрешения
/// </summary>
public class PermissionDto
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
    ///     Порядок сортировки
    /// </summary>
    public int SortValue { get; set; }

    /// <summary>
    ///     Название группы
    /// </summary>
    public string GroupName { get; set; } = null!;

    /// <summary>
    ///     Ключ группы
    /// </summary>
    public string GroupKey { get; set; } = null!;

    /// <summary>
    ///     Порядок сортировки группы
    /// </summary>
    public int GroupSortValue { get; set; }

    /// <summary>
    ///     Связан с ролью, для которой был запрос
    /// </summary>
    public bool IsRoleBound { get; set; }
}