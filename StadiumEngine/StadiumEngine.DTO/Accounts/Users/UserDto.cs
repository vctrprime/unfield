namespace StadiumEngine.DTO.Accounts.Users;

/// <summary>
///     ДТО пользователя
/// </summary>
public class UserDto : BaseEntityDto
{
    /// <summary>
    ///     Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Имя
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Фамилия
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    ///     Номер телефона
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    ///     Id роли
    /// </summary>
    public int? RoleId { get; set; }

    /// <summary>
    ///     Название роли
    /// </summary>
    public string? RoleName { get; set; }

    /// <summary>
    ///     Дата последнего входа
    /// </summary>
    public DateTime? LastLoginDate { get; set; }
    
    /// <summary>
    ///     Количество стадионов
    /// </summary>
    public int StadiumsCount { get; set; }
}