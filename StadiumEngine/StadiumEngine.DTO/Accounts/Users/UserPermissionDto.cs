namespace StadiumEngine.DTO.Accounts.Users;

/// <summary>
/// Разрешения для авторизованного пользователя
/// </summary>
public class UserPermissionDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///  Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Понятное имя
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Ключ группы разрешений
    /// </summary>
    public string GroupKey { get; set; }
}