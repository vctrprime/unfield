namespace Unfield.DTO;

/// <summary>
///     Базовое ДТо для сущностей
/// </summary>
public class BaseEntityDto
{
    /// <summary>
    ///     Дата создания
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    ///     Дата обновления
    /// </summary>
    public DateTime? DateModified { get; set; }

    /// <summary>
    ///     Пользователь создал
    /// </summary>
    public string? UserCreated { get; set; }

    /// <summary>
    ///     Пользователь обновил
    /// </summary>
    public string? UserModified { get; set; }
}