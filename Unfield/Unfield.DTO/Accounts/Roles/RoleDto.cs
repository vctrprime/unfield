namespace Unfield.DTO.Accounts.Roles;

/// <summary>
///     ДТО роли
/// </summary>
public class RoleDto : BaseEntityDto
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
    ///     Количество пользователей
    /// </summary>
    public int UsersCount { get; set; }

}