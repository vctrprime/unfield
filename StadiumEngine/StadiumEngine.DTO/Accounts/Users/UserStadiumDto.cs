namespace StadiumEngine.DTO.Accounts.Users;

/// <summary>
///     Доступные для пользователя стадионы
/// </summary>
public class UserStadiumDto
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
    ///     Адрес
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    ///     Теущий выбранный стадион
    /// </summary>
    public bool IsCurrent { get; set; }
}